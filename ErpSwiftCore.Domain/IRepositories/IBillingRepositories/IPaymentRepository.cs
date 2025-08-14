using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.ICore;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    /// <summary>
    /// مستودع المدفوعات (Payment aggregates).
    /// يرث عمليات CRUD الأساسية، الأرشفة، الحذف المنطقي، ودعم المتعدد المستأجرين من IMultiTenantRepository.
    /// يضيف استعلامات خاصة بالمدفوعات مثل جلب الدفعات بحسب الفاتورة أو النطاق الزمني والمبالغ، ودعم Paging/Filtering.
    /// </summary>
    public interface IPaymentRepository : IMultiTenantRepository<Payment>
    {
        // ----------- [CRUD & Bulk] -----------

        /// <summary>
        /// يضيف دفعة جديدة ويعيد الـ ID المنشأ
        /// </summary>
        Task<Guid> CreateAsync(
            Payment payment,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يضيف مجموعة دفعات ويعيد قائمة الـ IDs المنشأة
        /// </summary>
        Task<IEnumerable<Guid>> CreateRangeAsync(
            IEnumerable<Payment> payments,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يحدث دفعة موجودة
        /// </summary>
        Task<bool> UpdateAsync(
            Payment payment,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لدفعة بحسب الـ ID
        /// </summary>
        Task<bool> DeleteAsync(

            Guid paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة دفعات بناءً على قائمة IDs
        /// </summary>
        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> paymentIds,
            CancellationToken cancellationToken = default);

        // ----------- [Delete / Archive / Restore] -----------

        /// <summary>
        /// حذف منطقي لدفعة بحسب الـ ID
        /// </summary>
        Task<bool> SoftDeleteAsync(

            Guid paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة دفعات بناءً على قائمة IDs
        /// </summary>
        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> paymentIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة دفعة محذوفة منطقيًا
        /// </summary>
        Task<bool> RestoreAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة مجموعة دفعات محذوفة منطقيًا
        /// </summary>
        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> paymentIds,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------

        /// <summary>
        /// جلب دفعة بواسطة معرفها
        /// </summary>
        Task<Payment?> GetByIdAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب جميع الدفعات المتعلقة بفاتورة معينة
        /// </summary>
        Task<IReadOnlyList<Payment>> GetByInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------

        /// <summary>
        /// جلب جميع الدفعات
        /// </summary>
        Task<IReadOnlyList<Payment>> GetAllAsync(
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب الدفعات ضمن نطاق زمني (PaymentDate)
        /// </summary>
        Task<IReadOnlyList<Payment>> GetByDateRangeAsync(
            DateTime from,
            DateTime to,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// جلب الدفعات ضمن نطاق مبلغ معين
        /// </summary>
        Task<IReadOnlyList<Payment>> GetByAmountRangeAsync(
            decimal minAmount,
            decimal maxAmount,
            CancellationToken cancellationToken = default);

 

        // ----------- [Existence & Counts] -----------

        /// <summary>
        /// التحقق من وجود دفعة معينة
        /// </summary>
        Task<bool> ExistsAsync(
            Guid paymentId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// إحصاء عدد الدفعات (اختياري حسب الفاتورة أو الفترة أو النطاق المبلغ)
        /// </summary>
        Task<int> CountAsync(
            Guid? invoiceId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            decimal? minAmount = null,
            decimal? maxAmount = null,
            CancellationToken cancellationToken = default);
    }
}