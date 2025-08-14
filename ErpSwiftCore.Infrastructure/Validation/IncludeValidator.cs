// ملف: IncludeValidator.cs
using ErpSwiftCore.SharedKernel.Base;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using System.Security;

namespace ErpSwiftCore.Infrastructure.Validation
{
    /// <summary>
    /// Validates that Include expressions reference existing navigation properties on T,
    /// ويدعم التعابير المتداخلة (e.g. x => x.Category.SubCategory.CollectionProp).
    ///
    /// الشروط:
    /// 1. كل خاصية في المسار يجب أن تكون إما:
    ///    - كيانًا يرث من BaseEntity (مثل علاقة One-to-One أو One-to-Many)
    ///    - أو مجموعة مُولَّدة من كائن يرث BaseEntity (IEnumerable&lt;BaseEntityDerived&gt;)
    /// 2. نحظر تضمين الخصائص البسيطة (value types مثل int, string, DateTime إلخ).
    /// 3. إذا حُدِدَت مصفوفة (Array) أو أي نوع implements IEnumerable&lt;T&gt;،
    ///    نتحقق أن T يرث BaseEntity.
    /// </summary>
    /// <typeparam name="T">نوع الكيان (يرث من BaseEntity).</typeparam>
    public class IncludeValidator<T> : IIncludeValidator<T> where T : BaseEntity
    {
        // Cache سريعة لأسماء خصائص T (جذرية) لتسريع التحقّق الأولي
        private static readonly HashSet<string> _rootPropertyNames = new HashSet<string>(
            typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => p.Name),
            StringComparer.OrdinalIgnoreCase
        );

        // Cache للـ PropertyInfo لكل نوع نمرّ عليه في المستويات المتداخلة
        // (التخزين في ConcurrentDictionary لضمان thread-safe وسرعة للوصول متكرر)
        private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> _typePropertyCache
            = new ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>();

        public void Validate(params Expression<Func<T, object>>[] includes)
        {
            if (includes == null || includes.Length == 0)
                return;

            foreach (var inc in includes)
            {
                if (inc == null)
                    throw new SecurityException($"Include expression cannot be null.");

                // نُستخرج قائمة أسماء الخصائص المتسلسلة (مثلاً ["Category", "SubCategory", "Items"])
                var memberChain = GetMemberChain(inc.Body);

                if (memberChain.Count == 0)
                {
                    throw new SecurityException($"Unsupported include expression: '{inc}'.");
                }

                // نتحقق أن أول عنصر في السلسلة موجود في خصائص T
                var firstProp = memberChain[0];
                if (!_rootPropertyNames.Contains(firstProp))
                {
                    throw new SecurityException($"Property '{firstProp}' is not a valid navigation property on '{typeof(T).Name}'. Expression: '{inc}'.");
                }

                // نبدأ التحقّق المستمر في السلسلة
                Type currentType = typeof(T);

                for (int i = 0; i < memberChain.Count; i++)
                {
                    string propName = memberChain[i];
                    var propInfo = GetPropertyInfo(currentType, propName);
                    if (propInfo == null)
                    {
                        throw new SecurityException($"Invalid include property '{propName}' in expression '{inc}'.");
                    }

                    // نحصل على نوع الخواص
                    Type propertyType = propInfo.PropertyType;

                    bool isLastSegment = (i == memberChain.Count - 1);

                    if (isLastSegment)
                    {
                        // في آخر مستوى: يجب أن يكون propertyType يرث BaseEntity
                        // أو مجموعة IEnumerable<TDerived> حيث TDerived يرث BaseEntity
                        if (!IsEntityType(propertyType))
                        {
                            throw new SecurityException($"Property '{propName}' in expression '{inc}' is not a navigation property (not an entity or collection of entities).");
                        }
                    }
                    else
                    {
                        // ليس آخر مستوى: الخاصية يجب أن تؤدي إلى كيان أو مجموعة كيانات لتواصل المسار
                        if (IsEntityType(propertyType))
                        {
                            // إذا كانت خاصية كيان، ننتقل مباشرة
                            currentType = propertyType;
                        }
                        else if (IsCollectionOfEntity(propertyType, out Type? itemType))
                        {
                            // إذا كانت خاصية مجموعة، ننقل currentType إلى نوع العنصر (TDerived)
                            currentType = itemType!;
                        }
                        else
                        {
                            throw new SecurityException($"Intermediate property '{propName}' in expression '{inc}' is not a navigation property (cannot navigate through '{propertyType.Name}').");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// يستخرج قائمة أسماء الأعضاء في Include expression بترتيب المستويات
        /// (مثل ["Category", "SubCategory", "Items"]).
        /// يدعم MemberExpression المتداخل أو UnaryExpression الذي يحتوي MemberExpression.
        /// </summary>
        private static List<string> GetMemberChain(Expression expr)
        {
            var chain = new List<string>();

            // نزّل أي تحويل (UnaryExpression) حتى تصل إلى MemberExpression
            while (expr is UnaryExpression u && u.Operand is Expression operand)
            {
                expr = operand;
            }

            if (!(expr is MemberExpression me))
            {
                // إذا ليس MemberExpression → لا ندعم التعبير
                return chain;
            }

            // نتبع السلسلة صعودًا
            while (me != null)
            {
                chain.Add(me.Member.Name);
                if (me.Expression is MemberExpression parentMe)
                {
                    me = parentMe;
                }
                else if (me.Expression is ParameterExpression)
                {
                    break; // وصلنا إلى جذر التعبير (x => x.Property)
                }
                else
                {
                    break; // حالة غير متوقعة (كتعبير من نوع آخر)
                }
            }

            chain.Reverse();
            return chain;
        }

        /// <summary>
        /// يعيد PropertyInfo من الـ Cache أو يبحث في النوع الحالي.
        /// </summary>
        private static PropertyInfo? GetPropertyInfo(Type type, string propertyName)
        {
            var dict = _typePropertyCache.GetOrAdd(type, t =>
            {
                // Cache لكل خصائص النوع: مفتاح هو الاسم (Case-Insensitive)
                return t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);
            });

            dict.TryGetValue(propertyName, out var pi);
            return pi;
        }

        /// <summary>
        /// يتحقّق إن كان type يرث من BaseEntity مباشرةً.
        /// </summary>
        private static bool IsEntityType(Type type)
        {
            if (type == typeof(BaseEntity))
                return true;

            // تحقق من كون النوع يرث BaseEntity سواءً مباشرةً أو ضمنيا
            return typeof(BaseEntity).IsAssignableFrom(type);
        }

        /// <summary>
        /// يتحقّق إن كان type عبارة عن IEnumerable&lt;T&gt; حيث T يرث BaseEntity،
        /// أو مصفوفة T[] حيث T يرث BaseEntity.
        /// إذا وجدنا ذلك، نُرجع true ونُخرج itemType (نوع T).
        /// </summary>
        private static bool IsCollectionOfEntity(Type type, out Type? itemType)
        {
            itemType = null;

            // حالة المصفوفة: T[]
            if (type.IsArray)
            {
                var elemType = type.GetElementType();
                if (elemType != null && IsEntityType(elemType))
                {
                    itemType = elemType;
                    return true;
                }
                return false;
            }

            // نبحث عن واجهة IEnumerable<T>
            if (type.IsGenericType)
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsGenericType
                        && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        var genericArg = iface.GetGenericArguments()[0];
                        if (IsEntityType(genericArg))
                        {
                            itemType = genericArg;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}