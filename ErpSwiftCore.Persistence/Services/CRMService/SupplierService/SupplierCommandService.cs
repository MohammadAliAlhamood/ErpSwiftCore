using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ISupplierService;
using Microsoft.EntityFrameworkCore.Storage;
namespace ErpSwiftCore.Persistence.Services.CRMService.SupplierService
{
    public class SupplierCommandService : ISupplierCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly ISupplierValidationService _validationService;

        public SupplierCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            ISupplierValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

        // ----------- [Create] -----------

        public async Task<Guid> CreateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            // Required fields
            if (string.IsNullOrWhiteSpace(supplier.SupplierCode))
                throw new InvalidOperationException("SupplierCode is required.");
            if (string.IsNullOrWhiteSpace(supplier.NationalID))
                throw new InvalidOperationException("NationalID is required.");

            // Uniqueness checks via validation service
            bool isUnique = await _validationService.SupplierIsUniqueAsync(
                supplier.SupplierCode,
                supplier.ContactInfo?.Email,
                supplier.NationalID,
                supplier.ContactInfo?.Phone,
                cancellationToken);

            if (!isUnique)
                throw new InvalidOperationException("One or more supplier identifiers already exist.");

            supplier.CreatedAt = DateTime.UtcNow;
            var id = await _unitOfWork.Supplier.CreateAsync(supplier, cancellationToken);
            await _unitOfWork.SaveAsync();
            return id;
        }

        // ----------- [Bulk Create] -----------

        public async Task<IEnumerable<Guid>> AddSuppliersAsync(IEnumerable<Supplier> suppliers, CancellationToken cancellationToken = default)
        {
            if (suppliers == null)
                throw new ArgumentNullException(nameof(suppliers));

            var list = suppliers.ToList();
            var ids = new List<Guid>();

            using (IDbContextTransaction tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var supplier in list)
                    {
                        if (string.IsNullOrWhiteSpace(supplier.SupplierCode))
                            throw new InvalidOperationException("SupplierCode is required.");
                        if (string.IsNullOrWhiteSpace(supplier.NationalID))
                            throw new InvalidOperationException("NationalID is required.");

                        bool isUnique = await _validationService.SupplierIsUniqueAsync(
                            supplier.SupplierCode,
                            supplier.ContactInfo?.Email,
                            supplier.NationalID,
                            supplier.ContactInfo?.Phone,
                            cancellationToken);

                        if (!isUnique)
                            throw new InvalidOperationException("One or more supplier identifiers already exist.");

                        supplier.CreatedAt = DateTime.UtcNow;
                        var id = await _unitOfWork.Supplier.CreateAsync(supplier, cancellationToken);
                        ids.Add(id);
                    }

                    await _unitOfWork.SaveAsync();
                    await tx.CommitAsync(cancellationToken);
                    return ids;
                }
                catch
                {
                    await tx.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        // ----------- [Update] -----------

        public async Task<bool> UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            if (supplier == null)
                throw new ArgumentNullException(nameof(supplier));

            // Existence check via validation service
            if (!await _validationService.SupplierExistsAsync(supplier.ID, cancellationToken))
                return false;

            var existing = await _unitOfWork.Supplier.GetByIdAsync(supplier.ID, cancellationToken);
            if (existing == null)
                return false;

            // Uniqueness checks for update (with excludingId)
            bool isUnique = await _validationService.SupplierIsUniqueAsync(
                supplier.ID,
                supplier.SupplierCode,
                supplier.ContactInfo?.Email,
                supplier.NationalID,
                supplier.ContactInfo?.Phone,
                cancellationToken);

            if (!isUnique)
                throw new InvalidOperationException("One or more supplier identifiers already exist.");

            // Map updatable fields
            existing.FirstName = supplier.FirstName;
            existing.MiddleName = supplier.MiddleName;
            existing.LastName = supplier.LastName;
            existing.Gender = supplier.Gender;
            existing.NationalID = supplier.NationalID;
            existing.Address = supplier.Address;
            existing.ContactInfo = supplier.ContactInfo;
            existing.Notes = supplier.Notes;
            existing.SupplierCode = supplier.SupplierCode;
            existing.MaxSupplyLimit = supplier.MaxSupplyLimit;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = supplier.UpdatedBy;

            var success = await _unitOfWork.Supplier.UpdateAsync(existing, cancellationToken);
            if (success)
                await _unitOfWork.SaveAsync();

            return success;
        }

        // ----------- [Delete / Soft-Delete / Restore] -----------

        public async Task<bool> DeleteSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.SupplierExistsAsync(supplierId, cancellationToken))
                return false;

            var success = await _unitOfWork.Supplier.DeleteAsync(supplierId, cancellationToken);
            if (success)
                await _unitOfWork.SaveAsync();

            return success;
        }

        public async Task<bool> DeleteSuppliersRangeAsync(IEnumerable<Guid> supplierIds, CancellationToken cancellationToken = default)
        {
            if (supplierIds == null)
                return false;

            var list = supplierIds.ToList();
            using (var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    bool allDeleted = true;
                    foreach (var id in list)
                        if (!await DeleteSupplierAsync(id, cancellationToken))
                            allDeleted = false;

                    await tx.CommitAsync(cancellationToken);
                    return allDeleted;
                }
                catch
                {
                    await tx.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAllSuppliersAsync(CancellationToken cancellationToken = default)
        {
            var allActive = await _unitOfWork.Supplier.GetAllAsync(null, cancellationToken);
            return await DeleteSuppliersRangeAsync(allActive.Select(s => s.ID), cancellationToken);
        }

        public async Task<bool> RestoreSupplierAsync(Guid supplierId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.SupplierExistsAsync(supplierId, cancellationToken))
                return false;

            var success = await _unitOfWork.Supplier.RestoreAsync(supplierId, cancellationToken);
            if (success)
                await _unitOfWork.SaveAsync();

            return success;
        }

        public async Task<bool> RestoreSuppliersRangeAsync(IEnumerable<Guid> supplierIds, CancellationToken cancellationToken = default)
        {
            if (supplierIds == null)
                return false;

            var list = supplierIds.ToList();
            using (var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    bool allRestored = true;
                    foreach (var id in list)
                        if (!await RestoreSupplierAsync(id, cancellationToken))
                            allRestored = false;

                    await tx.CommitAsync(cancellationToken);
                    return allRestored;
                }
                catch
                {
                    await tx.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

        public async Task<bool> RestoreAllSuppliersAsync(CancellationToken cancellationToken = default)
        {
            var allSoftDeleted = await _unitOfWork.Supplier.GetAllSoftDeletedAsync(null, cancellationToken);
            return await RestoreSuppliersRangeAsync(allSoftDeleted.Select(s => s.ID), cancellationToken);
        }
    }
}
