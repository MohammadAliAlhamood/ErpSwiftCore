using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع الطلبات (Orders)
    /// يرث عمليات CRUD الأساسية، الأرشفة، الحذف المنطقي، ودعم المتعدد المستأجرين من IMultiTenantRepository.
    /// يضيف استعلامات خاصة بالطلبات مثل جلب الطلبات بحسب الطرف، النوع، الحالة، وتنقيح Paging/Filtering.
    /// </summary>
    public interface IOrderRepository : IMultiTenantRepository<Order>
    {
        // ----------- [CRUD & Bulk] -----------

        /// <summary>
        /// يضيف طلباً جديداً ويعيد الـ ID المنشأ
        /// </summary>
        Task<Guid> CreateAsync(
            Order order,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يضيف مجموعة طلبات ويعيد قائمة الـ IDs المنشأة
        /// </summary>
        Task<IEnumerable<Guid>> AddRangeAsync(
            IEnumerable<Order> orders,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يحدث طلباً موجوداً
        /// </summary>
        Task<bool> UpdateAsync(
            Order order,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لطلب بحسب الـ ID
        /// </summary>
        Task<bool> DeleteAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة طلبات بناءً على قائمة IDs
        /// </summary>
        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> orderIds,
            CancellationToken cancellationToken = default);

        // ----------- [Delete / Archive / Restore] -----------

        /// <summary>
        /// حذف منطقي لطلب بحسب الـ ID
        /// </summary>
        Task<bool> SoftDeleteAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة طلبات بناءً على قائمة IDs
        /// </summary>
        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> orderIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة طلب محذوف منطقيًا
        /// </summary>
        Task<bool> RestoreAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة مجموعة طلبات محذوفة منطقيًا
        /// </summary>
        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> orderIds,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------

        /// <summary>
        /// جلب طلب بواسطة معرفه
        /// </summary>
        Task<Order?> GetByIdAsync(
            Guid orderId,
            CancellationToken cancellationToken = default);

         
        

        // ----------- [Get / Query - Bulk / Advanced] -----------

        /// <summary>
        /// جلب جميع الطلبات
        /// </summary>
        Task<IReadOnlyList<Order>> GetAllAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب الطلبات ضمن نطاق زمني بناءً على OrderDate
        /// </summary>
        Task<IReadOnlyList<Order>> GetByDateRangeAsync(
            DateTime from,
            DateTime to,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب الطلبات حسب النوع (Sales أو Purchase)
        /// </summary>
        Task<IReadOnlyList<Order>> GetByTypeAsync(
            OrderType orderType,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب الطلبات حسب الحالة (Draft, Confirmed, Approved, ...)
        /// </summary>
        Task<IReadOnlyList<Order>> GetByStatusAsync(
            OrderStatus orderStatus,
            CancellationToken cancellationToken = default);

        // ----------- [Existence & Counts] -----------

        /// <summary>
        /// التحقق من وجود طلب معين
        /// </summary>
        Task<bool> ExistsAsync(Guid orderId, CancellationToken cancellationToken = default);

        /// <summary>
        /// إحصاء عدد الطلبات (اختياري حسب الطرف أو النوع أو الحالة أو النطاق الزمني)
        /// </summary>
        Task<int> CountAsync(
            Guid? partyId = null, 
            OrderType? orderType = null,
            OrderStatus? status = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            CancellationToken cancellationToken = default);
    }
}