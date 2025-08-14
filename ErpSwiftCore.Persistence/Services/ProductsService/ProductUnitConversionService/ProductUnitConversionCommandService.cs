using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.Entities.EntityProduct;
using ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService;
namespace ErpSwiftCore.Persistence.Services.ProductsService.ProductUnitConversionService
{
    public class ProductUnitConversionCommandService : IProductUnitConversionCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IProductUnitConversionValidationService _validation;
        public ProductUnitConversionCommandService(IMultiTenantUnitOfWork unitOfWork, IProductUnitConversionValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }
        public async Task<Guid> CreateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default)
        {
            // تحقق من صحة المنتج والوحدات والنسبة والعامل
            if (!await _validation.IsValidProductAsync(conversion.ProductId, cancellationToken))
                throw new InvalidOperationException("المنتج غير صالح أو غير موجود.");
            if (!await _validation.IsValidUnitAsync(conversion.FromUnitId, cancellationToken) || !await _validation.IsValidUnitAsync(conversion.ToUnitId, cancellationToken))
                throw new InvalidOperationException("الوحدة غير صالحة.");
            if (!await _validation.IsValidConversionRateAsync(conversion.ConversionRate, cancellationToken))
                throw new InvalidOperationException("معدل التحويل غير صالح.");
            if (!await _validation.IsValidFactorAsync(conversion.Factor, cancellationToken))
                throw new InvalidOperationException("العامل غير صالح.");
            if (await _validation.UnitConversionExistsForProductAsync(conversion.ProductId, conversion.FromUnitId, conversion.ToUnitId, null, cancellationToken))
                throw new InvalidOperationException("يوجد تحويل لنفس المنتج والوحدات.");
            var id = await _unitOfWork.ProductUnitConversion.CreateAsync(conversion, cancellationToken);
            return id;
        }
        public async Task<IEnumerable<Guid>> AddUnitConversionsRangeAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.AddRangeAsync(conversions, cancellationToken);
        }
        public async Task<bool> UpdateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default)
        {
            if (!await _validation.UnitConversionExistsByIdAsync(conversion.ID, cancellationToken))
                throw new InvalidOperationException("تحويل الوحدة غير موجود.");
            if (!await _validation.IsValidProductAsync(conversion.ProductId, cancellationToken))
                throw new InvalidOperationException("المنتج غير صالح أو غير موجود.");
            if (!await _validation.IsValidUnitAsync(conversion.FromUnitId, cancellationToken) || !await _validation.IsValidUnitAsync(conversion.ToUnitId, cancellationToken))
                throw new InvalidOperationException("الوحدة غير صالحة.");
            if (!await _validation.IsValidConversionRateAsync(conversion.ConversionRate, cancellationToken))
                throw new InvalidOperationException("معدل التحويل غير صالح.");
            if (!await _validation.IsValidFactorAsync(conversion.Factor, cancellationToken))
                throw new InvalidOperationException("العامل غير صالح.");
            if (await _validation.UnitConversionExistsForProductAsync(conversion.ProductId, conversion.FromUnitId, conversion.ToUnitId, conversion.ID, cancellationToken))
                throw new InvalidOperationException("يوجد تحويل لنفس المنتج والوحدات.");
            var result = await _unitOfWork.ProductUnitConversion.UpdateAsync(conversion, cancellationToken);
            return result;
        }
        public async Task<bool> DeleteUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.DeleteAsync(conversionId, cancellationToken);
        }
        public async Task<bool> DeleteUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.DeleteRangeAsync(conversionIds, cancellationToken);
        }
        public async Task<bool> DeleteAllUnitConversionsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.DeleteAllAsync(cancellationToken);
        }
        public async Task<bool> RestoreUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.RestoreAsync(conversionId, cancellationToken);
        }
        public async Task<bool> RestoreUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.RestoreRangeAsync(conversionIds, cancellationToken);
        }
        public async Task<bool> RestoreAllUnitConversionsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.RestoreAllAsync(cancellationToken);
        }
        

        public async Task<int> BulkImportUnitConversionsAsync(IEnumerable<ProductUnitConversion> conversions, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.BulkImportAsync(conversions, cancellationToken);
        }
        public async Task<int> BulkDeleteUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.BulkDeleteAsync(conversionIds, cancellationToken);
        }
        public async Task<bool> SoftDeleteUnitConversionAsync(Guid conversionId, CancellationToken cancellationToken = default)
        {
            // تأكد من وجود التحويل
            if (!await _validation.UnitConversionExistsByIdAsync(conversionId, cancellationToken))
                throw new InvalidOperationException("تحويل الوحدة غير موجود.");
            return await _unitOfWork.ProductUnitConversion.SoftDeleteAsync(conversionId, cancellationToken);
        }

        public async Task<bool> SoftDeleteUnitConversionsRangeAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.SoftDeleteRangeAsync(conversionIds, cancellationToken);
        }

        public async Task<bool> SoftDeleteAllUnitConversionsAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.ProductUnitConversion.SoftDeleteAllAsync(cancellationToken);
        }

        public async Task<int> BulkSoftDeleteUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.ProductUnitConversion.SoftDeleteRangeAsync(conversionIds, cancellationToken);
            return succeeded ? conversionIds.Count() : 0;
        }
        public async Task<int> BulkRestoreUnitConversionsAsync(IEnumerable<Guid> conversionIds, CancellationToken cancellationToken = default)
        {
            var succeeded = await _unitOfWork.ProductUnitConversion.RestoreRangeAsync(conversionIds, cancellationToken);
            return succeeded ? conversionIds.Count() : 0;
        }

    }
}
