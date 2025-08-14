using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IInvoiceTaxDiscountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.BillingService.InvoiceTaxDiscountService
{
    public class InvoiceTaxDiscountCommandService : IInvoiceTaxDiscountCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IInvoiceTaxDiscountValidationService _validation;
        public InvoiceTaxDiscountCommandService(IMultiTenantUnitOfWork unitOfWork, IInvoiceTaxDiscountValidationService validation)
        {
            _unitOfWork = unitOfWork;
            _validation = validation;
        }

        // -------------------- [Create Tax & Discount] --------------------

        public async Task<InvoiceDiscount> AddDiscountAsync(
            Guid invoiceId,
            InvoiceDiscount discount,
            CancellationToken cancellationToken = default)
        {
            if (discount == null) throw new ArgumentNullException(nameof(discount));

            // Ensure invoice exists
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                discount.InvoiceId = invoiceId;
                var newId = await _unitOfWork.InvoiceDiscount.CreateAsync(discount, cancellationToken);
                discount.ID = newId;

    
                await tx.CommitAsync(cancellationToken);
                return discount;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<InvoiceDiscount>> AddDiscountsAsync(
            Guid invoiceId,
            IEnumerable<InvoiceDiscount> discounts,
            CancellationToken cancellationToken = default)
        {
            if (discounts == null) throw new ArgumentNullException(nameof(discounts));

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var toAdd = discounts.ToList();
            var created = new List<InvoiceDiscount>();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var discount in toAdd)
                {
                    discount.InvoiceId = invoiceId;
                    var newId = await _unitOfWork.InvoiceDiscount.CreateAsync(discount, cancellationToken);
                    discount.ID = newId;
                    created.Add(discount);
                     
                } 
                await tx.CommitAsync(cancellationToken);
                return created;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<InvoiceTax> AddTaxAsync(
            Guid invoiceId,
            InvoiceTax tax,
            CancellationToken cancellationToken = default)
        {
            if (tax == null) throw new ArgumentNullException(nameof(tax));

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                tax.InvoiceId = invoiceId;
                var newId = await _unitOfWork.InvoiceTax.CreateAsync(tax, cancellationToken);
                tax.ID = newId;
 
                await tx.CommitAsync(cancellationToken);
                return tax;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<InvoiceTax>> AddTaxesAsync(
            Guid invoiceId,
            IEnumerable<InvoiceTax> taxes,
            CancellationToken cancellationToken = default)
        {
            if (taxes == null) throw new ArgumentNullException(nameof(taxes));

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var toAdd = taxes.ToList();
            var created = new List<InvoiceTax>();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var tax in toAdd)
                {
                    tax.InvoiceId = invoiceId;
                    var newId = await _unitOfWork.InvoiceTax.CreateAsync(tax, cancellationToken);
                    tax.ID = newId;
                    created.Add(tax);

                   
                } 
                await tx.CommitAsync(cancellationToken);
                return created;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<(IEnumerable<InvoiceTax> Taxes, IEnumerable<InvoiceDiscount> Discounts)> CreateTaxesAndDiscountsAsync(
            Guid invoiceId,
            IEnumerable<InvoiceTax> taxes,
            IEnumerable<InvoiceDiscount> discounts,
            CancellationToken cancellationToken = default)
        {
            if (taxes == null && discounts == null)
                throw new ArgumentException("Either taxes or discounts must be provided.");

            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var taxa = taxes?.ToList() ?? new List<InvoiceTax>();
            var disca = discounts?.ToList() ?? new List<InvoiceDiscount>();
            var createdTaxes = new List<InvoiceTax>();
            var createdDiscounts = new List<InvoiceDiscount>();

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Create taxes
                foreach (var tax in taxa)
                {
                    tax.InvoiceId = invoiceId;
                    var newId = await _unitOfWork.InvoiceTax.CreateAsync(tax, cancellationToken);
                    tax.ID = newId;
                    createdTaxes.Add(tax);
                     
                }

                // Create discounts
                foreach (var discount in disca)
                {
                    discount.InvoiceId = invoiceId;
                    var newId = await _unitOfWork.InvoiceDiscount.CreateAsync(discount, cancellationToken);
                    discount.ID = newId;
                    createdDiscounts.Add(discount);
                     
                }

                await _unitOfWork.SaveAsync();
                await tx.CommitAsync(cancellationToken);
                return (createdTaxes, createdDiscounts);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // -------------------- [Get & Exists] --------------------

        // -------------------- [Delete & Restore] --------------------

        public async Task<bool> DeleteDiscountAsync(Guid discountId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.DiscountExistsAsync(discountId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceDiscount.GetByIdAsync(discountId, cancellationToken);
                var oldJson = JsonSerializer.Serialize(existing);

                var success = await _unitOfWork.InvoiceDiscount.DeleteAsync(discountId, cancellationToken);
 
                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteTaxAsync(Guid taxId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.TaxExistsAsync(taxId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceTax.GetByIdAsync(taxId, cancellationToken);
                var oldJson = JsonSerializer.Serialize(existing);

                var success = await _unitOfWork.InvoiceTax.DeleteAsync(taxId, cancellationToken);
                 
                await tx.CommitAsync(cancellationToken);
                return success;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteAllDiscountsOfInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default)
        {
            var discounts = await _unitOfWork.InvoiceDiscount.GetByInvoiceAsync(invoiceId, cancellationToken);
            if (!discounts.Any()) return true;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var d in discounts)
                {
                    var oldJson = JsonSerializer.Serialize(d);
                    await _unitOfWork.InvoiceDiscount.DeleteAsync(d.ID, cancellationToken);
                     
                }

                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteAllTaxesOfInvoiceAsync(
            Guid invoiceId,
            CancellationToken cancellationToken = default)
        {
            var taxes = await _unitOfWork.InvoiceTax.GetByInvoiceAsync(invoiceId, cancellationToken);
            if (!taxes.Any()) return true;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var t in taxes)
                {
                    var oldJson = JsonSerializer.Serialize(t);
                    await _unitOfWork.InvoiceTax.DeleteAsync(t.ID, cancellationToken);

                   
                }
                 
                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        // -------------------- [Totals & Search] --------------------


        // -------------------- [Update] --------------------

        public async Task<InvoiceDiscount> UpdateDiscountAsync(
            InvoiceDiscount discount,
            CancellationToken cancellationToken = default)
        {
            if (discount == null) throw new ArgumentNullException(nameof(discount));
            if (!await _validation.DiscountExistsAsync(discount.ID, cancellationToken))
                throw new InvalidOperationException("Discount not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceDiscount.GetByIdAsync(discount.ID, cancellationToken);
                if (existing == null) throw new InvalidOperationException("Discount not found.");
                var oldJson = JsonSerializer.Serialize(existing);

                var success = await _unitOfWork.InvoiceDiscount.UpdateAsync(discount, cancellationToken);
 
                await _unitOfWork.SaveAsync();
                await tx.CommitAsync(cancellationToken);
                return discount;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<InvoiceTax> UpdateTaxAsync(
            InvoiceTax tax,
            CancellationToken cancellationToken = default)
        {
            if (tax == null) throw new ArgumentNullException(nameof(tax));
            if (!await _validation.TaxExistsAsync(tax.ID, cancellationToken))
                throw new InvalidOperationException("Tax not found.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.InvoiceTax.GetByIdAsync(tax.ID, cancellationToken);
                if (existing == null) throw new InvalidOperationException("Tax not found.");
                var oldJson = JsonSerializer.Serialize(existing);

                var success = await _unitOfWork.InvoiceTax.UpdateAsync(tax, cancellationToken);

                
                await tx.CommitAsync(cancellationToken);
                return tax;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<(IEnumerable<InvoiceTax> UpdatedTaxes, IEnumerable<InvoiceDiscount> UpdatedDiscounts)> UpdateTaxesAndDiscountsAsync(
            Guid invoiceId,
            IEnumerable<InvoiceTax>? taxesToAdd = null,
            IEnumerable<InvoiceTax>? taxesToUpdate = null,
            IEnumerable<Guid>? taxIdsToDelete = null,
            IEnumerable<InvoiceDiscount>? discountsToAdd = null,
            IEnumerable<InvoiceDiscount>? discountsToUpdate = null,
            IEnumerable<Guid>? discountIdsToDelete = null,
            CancellationToken cancellationToken = default)
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(invoiceId, cancellationToken);
            if (invoice == null)
                throw new InvalidOperationException($"Invoice '{invoiceId}' not found.");

            var addedTaxes = new List<InvoiceTax>();
            var addedDiscounts = new List<InvoiceDiscount>();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // Delete taxes
                if (taxIdsToDelete != null)
                {
                    foreach (var taxId in taxIdsToDelete)
                    {
                        if (await _validation.TaxExistsAsync(taxId, cancellationToken))
                        {
                            var existingTax = await _unitOfWork.InvoiceTax.GetByIdAsync(taxId, cancellationToken);
                            var oldJsonTax = JsonSerializer.Serialize(existingTax);
                            await _unitOfWork.InvoiceTax.DeleteAsync(taxId, cancellationToken);
                             
                        }
                    }
                }

                // Update taxes
                if (taxesToUpdate != null)
                {
                    foreach (var tax in taxesToUpdate)
                    {
                        if (await _validation.TaxExistsAsync(tax.ID, cancellationToken))
                        {
                            var existingTax = await _unitOfWork.InvoiceTax.GetByIdAsync(tax.ID, cancellationToken);
                            var oldJsonTax = JsonSerializer.Serialize(existingTax);
                            await _unitOfWork.InvoiceTax.UpdateAsync(tax, cancellationToken);
                             
                        }
                    }
                }

                // Add new taxes
                if (taxesToAdd != null)
                {
                    foreach (var tax in taxesToAdd)
                    {
                        tax.InvoiceId = invoiceId;
                        var newId = await _unitOfWork.InvoiceTax.CreateAsync(tax, cancellationToken);
                        tax.ID = newId;
                        addedTaxes.Add(tax);
                         
                    }
                }

                // Delete discounts
                if (discountIdsToDelete != null)
                {
                    foreach (var discountId in discountIdsToDelete)
                    {
                        if (await _validation.DiscountExistsAsync(discountId, cancellationToken))
                        {
                            var existingDisc = await _unitOfWork.InvoiceDiscount.GetByIdAsync(discountId, cancellationToken);
                            var oldJsonDisc = JsonSerializer.Serialize(existingDisc);
                            await _unitOfWork.InvoiceDiscount.DeleteAsync(discountId, cancellationToken);
                             
                        }
                    }
                }

                // Update discounts
                if (discountsToUpdate != null)
                {
                    foreach (var discount in discountsToUpdate)
                    {
                        if (await _validation.DiscountExistsAsync(discount.ID, cancellationToken))
                        {
                            var existingDisc = await _unitOfWork.InvoiceDiscount.GetByIdAsync(discount.ID, cancellationToken);
                            var oldJsonDisc = JsonSerializer.Serialize(existingDisc);
                            await _unitOfWork.InvoiceDiscount.UpdateAsync(discount, cancellationToken);
                             
                        }
                    }
                }

                // Add new discounts
                if (discountsToAdd != null)
                {
                    foreach (var discount in discountsToAdd)
                    {
                        discount.InvoiceId = invoiceId;
                        var newId = await _unitOfWork.InvoiceDiscount.CreateAsync(discount, cancellationToken);
                        discount.ID = newId;
                        addedDiscounts.Add(discount); 
                    }
                }

                await _unitOfWork.SaveAsync();
                await tx.CommitAsync(cancellationToken);
                return (addedTaxes, addedDiscounts);
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        } 


    }
}