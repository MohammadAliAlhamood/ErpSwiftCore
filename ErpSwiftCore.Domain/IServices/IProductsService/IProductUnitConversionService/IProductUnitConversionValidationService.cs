using ErpSwiftCore.Domain.Entities.EntityProduct;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.IProductsService.IProductUnitConversionService
{
    /// <summary>
    /// Validation service for ProductUnitConversion entity.
    /// Ensures existence, referential integrity, business rules and aggregate consistency.
    /// </summary>
    public interface IProductUnitConversionValidationService
    {
        #region Existence & Referential Integrity

        /// <summary>
        /// تحقق من وجود سجل التحويل حسب المعرف.
        /// </summary>
        Task<bool> UnitConversionExistsByIdAsync(Guid conversionId, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من عدم وجود تحويل مكرر لنفس المنتج ونفس الوحدتين (باستثناء تحويل محدد للتحديث).
        /// </summary>
        Task<bool> UnitConversionExistsForProductAsync(Guid productId, Guid fromUnitId, Guid toUnitId, Guid? excludeConversionId = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من صلاحية معرف المنتج (وجوده وعدم حذفه ناعماً).
        /// </summary>
        Task<bool> IsValidProductAsync(Guid productId, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من صلاحية معرف الوحدة (وجودها وعدم حذفها ناعماً).
        /// </summary>
        Task<bool> IsValidUnitAsync(Guid unitId, CancellationToken cancellationToken = default);

        #endregion
        #region Business Rules Validation

        /// <summary>
        /// تحقق من أن معدل التحويل موجـب (> 0).
        /// </summary>
        Task<bool> IsValidConversionRateAsync(decimal conversionRate, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من أن العامل موجـب (> 0).
        /// </summary>
        Task<bool> IsValidFactorAsync(decimal factor, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من عدم تحويل الوحدة إلى نفسها (fromUnitId != toUnitId).
        /// </summary>
        Task<bool> IsNotSelfConversionAsync(Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default);

        /// <summary>
        /// تحقق من عدم وجود تحويل عكسي بالفعل (toUnit → fromUnit).
        /// </summary>
        Task<bool> IsNotReverseConversionExistsAsync(Guid productId, Guid fromUnitId, Guid toUnitId, CancellationToken cancellationToken = default);

        #endregion
        #region Aggregate Validation

        /// <summary>
        /// تنفيذ جميع عمليات التحقق قبل إنشاء أو تحديث تحويل وحدة:
        /// - صلاحية المنتج والوحدات
        /// - عدم تحويل الوحدة إلى نفسها
        /// - معدل التحويل والعامل موجـبان
        /// - عدم وجود تكرار أو تحويل عكسي
        /// </summary>
        Task<bool> ValidateUnitConversionAsync(ProductUnitConversion conversion, CancellationToken cancellationToken = default);

        #endregion
    }
}
