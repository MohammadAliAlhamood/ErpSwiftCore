using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductCategoryService
{
    /// <summary>
    /// Validation service for ProductCategory entity.
    /// Ensures existence, uniqueness, referential integrity, business rules and data consistency.
    /// </summary>
    public interface IProductCategoryValidationService
    {
        #region Existence & Referential Integrity

        /// <summary>
        /// تحقق من وجود الفئة حسب المعرف.
        /// </summary>
        Task<bool> CategoryExistsByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من وجود فئة بنفس الاسم تحت نفس الأب (أو على المستوى الأعلى إذا parentCategoryId = null).
        /// </summary>
        Task<bool> CategoryExistsByNameAsync(string name, Guid? parentCategoryId = null,
            Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من أن الاسم فريد ضمن نفس الفئة الأم.
        /// </summary>
        Task<bool> IsCategoryNameUniqueAsync(string name, Guid? parentCategoryId = null, 
            Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من صلاحية معرف الفئة الأب (وجوده وعدم الإشارة إلى الذات).
        /// </summary>
        Task<bool> IsValidParentCategoryAsync(Guid? parentCategoryId,
            Guid? excludeCategoryId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من عدم وجود حلقات تسلسلية (circular dependency) عند تغيير الأب.
        /// </summary>
        Task<bool> ExistsCircularDependencyAsync(Guid categoryId, 
            Guid? parentCategoryId, CancellationToken cancellationToken = default);

        #endregion

        #region Business Rules Validation
         
        Task<bool> IsNameValidAsync(string name, CancellationToken cancellationToken = default);

        #endregion

        #region Aggregate Validation 
        Task<bool> ValidateCategoryAsync(ProductCategory category, CancellationToken cancellationToken = default);

        #endregion
    }
}
