using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IRepositories.IBillingRepositories
{
    public interface IPartyRepository : IMultiTenantRepository<Party>
    {
        // ----------- [CRUD & Bulk] -----------

        /// <summary>
        /// يضيف دفعة جديدة ويعيد الـ ID المنشأ
        /// </summary>
        Task<Guid> CreateAsync(
            Party Party,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يضيف مجموعة دفعات ويعيد قائمة الـ IDs المنشأة
        /// </summary>
        Task<IEnumerable<Guid>> CreateRangeAsync(
            IEnumerable<Party> Partys,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// يحدث دفعة موجودة
        /// </summary>
        Task<bool> UpdateAsync(
            Party Party,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لدفعة بحسب الـ ID
        /// </summary>
        Task<bool> DeleteAsync(

            Guid PartyId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة دفعات بناءً على قائمة IDs
        /// </summary>
        Task<bool> DeleteRangeAsync(
            IEnumerable<Guid> PartyIds,
            CancellationToken cancellationToken = default);

        // ----------- [Delete / Archive / Restore] -----------

        /// <summary>
        /// حذف منطقي لدفعة بحسب الـ ID
        /// </summary>
        Task<bool> SoftDeleteAsync(

            Guid PartyId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// حذف منطقي لمجموعة دفعات بناءً على قائمة IDs
        /// </summary>
        Task<bool> SoftDeleteRangeAsync(
            IEnumerable<Guid> PartyIds,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة دفعة محذوفة منطقيًا
        /// </summary>
        Task<bool> RestoreAsync(
            Guid PartyId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// استعادة مجموعة دفعات محذوفة منطقيًا
        /// </summary>
        Task<bool> RestoreRangeAsync(
            IEnumerable<Guid> PartyIds,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Single] -----------

        /// <summary>
        /// جلب دفعة بواسطة معرفها
        /// </summary>
        Task<Party?> GetByIdAsync(
            Guid PartyId,
            CancellationToken cancellationToken = default);

        // ----------- [Get / Query - Bulk / Advanced] -----------

        /// <summary>
        /// جلب جميع الدفعات
        /// </summary>
        Task<IReadOnlyList<Party>> GetAllAsync(
            CancellationToken cancellationToken = default);

         

        // ----------- [Existence & Counts] -----------

        /// <summary>
        /// التحقق من وجود دفعة معينة
        /// </summary>
        Task<bool> ExistsAsync(
            Guid PartyId,
            CancellationToken cancellationToken = default);
         
    }
}