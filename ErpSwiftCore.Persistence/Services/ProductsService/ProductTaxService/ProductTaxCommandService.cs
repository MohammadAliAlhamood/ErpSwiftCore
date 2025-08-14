using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductTaxService;

namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductTaxService
{
    public class ProductTaxCommandService : IProductTaxCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductTaxValidationService _validation;

        public ProductTaxCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            IProductTaxValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Guid> CreateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default)
        {
            if (!await _validation.IsValidProductAsync(tax.ProductId, cancellationToken))
                throw new InvalidOperationException("المنتج غير صالح أو غير موجود.");
            if (!await _validation.IsValidRateAsync(tax.Rate, cancellationToken))
                throw new InvalidOperationException("نسبة الضريبة غير صالحة.");
            if (await _validation.TaxExistsForProductAsync(tax.ProductId, tax.Rate, null, cancellationToken))
                throw new InvalidOperationException("يوجد ضريبة بنفس النسبة لهذا المنتج.");
            return await _unitOfWork.ProductTax.CreateAsync(tax, cancellationToken);
        }

        public async Task<IEnumerable<Guid>> AddTaxesRangeAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default)
        {
            foreach (var tax in taxes)
            {
                if (!await _validation.ValidateTaxAsync(tax, cancellationToken))
                    throw new InvalidOperationException($"التحقق فشل للضريبة الخاصة بالمنتج {tax.ProductId} بمعدل {tax.Rate}.");
            }
            return await _unitOfWork.ProductTax.AddRangeAsync(taxes, cancellationToken);
        }

        public async Task<bool> UpdateTaxAsync(ProductTax tax, CancellationToken cancellationToken = default)
        {
            if (!await _validation.TaxExistsByIdAsync(tax.ID, cancellationToken))
                throw new InvalidOperationException("الضريبة غير موجودة.");
            if (!await _validation.ValidateTaxAsync(tax, cancellationToken))
                throw new InvalidOperationException("التحقق من الضريبة فشل.");
            return await _unitOfWork.ProductTax.UpdateAsync(tax, cancellationToken);
        }

        public async Task<bool> DeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.DeleteAsync(taxId, cancellationToken);
        }

        public async Task<bool> DeleteTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.DeleteRangeAsync(taxIds, cancellationToken);
        }

        public async Task<bool> DeleteAllTaxesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.DeleteAllAsync(cancellationToken);
        }

        public async Task<bool> RestoreTaxAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.RestoreAsync(taxId, cancellationToken);
        }
        public async Task<bool> RestoreTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.RestoreRangeAsync(taxIds, cancellationToken);
        }
        public async Task<bool> RestoreAllTaxesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.RestoreAllAsync(cancellationToken);
        }

        public async Task<int> BulkImportTaxesAsync(IEnumerable<ProductTax> taxes, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.BulkImportAsync(taxes, cancellationToken);
        }
        public async Task<int> BulkDeleteTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.BulkDeleteAsync(taxIds, cancellationToken);
        }
        
        public async Task<bool> SoftDeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.TaxExistsByIdAsync(taxId, cancellationToken))
                throw new InvalidOperationException("الضريبة غير موجودة.");
            return await _unitOfWork.ProductTax.SoftDeleteAsync(taxId, cancellationToken);
        }
        public async Task<bool> SoftDeleteTaxesRangeAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.SoftDeleteRangeAsync(taxIds, cancellationToken);
        }
        public async Task<bool> SoftDeleteAllTaxesAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductTax.SoftDeleteAllAsync(cancellationToken);
        }
        public async Task<int> BulkSoftDeleteTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.ProductTax.SoftDeleteRangeAsync(taxIds, cancellationToken);
            return succeeded ? taxIds.Count() : 0;
        }
        public async Task<int> BulkRestoreTaxesAsync(IEnumerable<Guid> taxIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.ProductTax.RestoreRangeAsync(taxIds, cancellationToken);
            return succeeded ? taxIds.Count() : 0;
        }
    }
}
