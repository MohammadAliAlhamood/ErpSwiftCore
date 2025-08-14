using ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Billings.InvoiceTaxDiscounts.Commands
{

    /// <summary>
    /// 1. Create taxes and discounts for an invoice
    /// </summary>
    public class CreateTaxesAndDiscountsCommand : IRequest<APIResponseDto>
    {
        public CreateTaxesAndDiscountsDto Dto { get; }
        public CreateTaxesAndDiscountsCommand(CreateTaxesAndDiscountsDto dto) => Dto = dto;
    }

    /// <summary>
    /// 2. Update taxes and discounts for an invoice
    /// </summary>
    public class UpdateTaxesAndDiscountsCommand : IRequest<APIResponseDto>
    {
        public UpdateTaxesAndDiscountsDto Dto { get; }
        public UpdateTaxesAndDiscountsCommand(UpdateTaxesAndDiscountsDto dto) => Dto = dto;
    }

    /// <summary>
    /// 3. Add a single tax to an invoice
    /// </summary>
    public class AddInvoiceTaxCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CreateInvoiceTaxDto Dto { get; }

        public AddInvoiceTaxCommand(Guid invoiceId, CreateInvoiceTaxDto dto)
        {
            InvoiceId = invoiceId;
            Dto = dto;
        }
    }

    /// <summary>
    /// 4. Add multiple taxes to an invoice
    /// </summary>
    public class AddInvoiceTaxesCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public IEnumerable<CreateInvoiceTaxDto> Dtos { get; }

        public AddInvoiceTaxesCommand(Guid invoiceId, IEnumerable<CreateInvoiceTaxDto> dtos)
        {
            InvoiceId = invoiceId;
            Dtos = dtos;
        }
    }

    /// <summary>
    /// 5. Update a tax
    /// </summary>
    public class UpdateInvoiceTaxCommand : IRequest<APIResponseDto>
    {
        public UpdateInvoiceTaxDto Dto { get; }
        public UpdateInvoiceTaxCommand(UpdateInvoiceTaxDto dto) => Dto = dto;
    }

    /// <summary>
    /// 6. Delete a tax
    /// </summary>
    public class DeleteInvoiceTaxCommand : IRequest<APIResponseDto>
    {
        public Guid TaxId { get; }
        public DeleteInvoiceTaxCommand(Guid taxId) => TaxId = taxId;
    }

    /// <summary>
    /// 7. Delete all taxes of an invoice
    /// </summary>
    public class DeleteAllTaxesOfInvoiceCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public DeleteAllTaxesOfInvoiceCommand(Guid invoiceId) => InvoiceId = invoiceId;
    }

    /// <summary>
    /// 8. Add a single discount to an invoice
    /// </summary>
    public class AddInvoiceDiscountCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public CreateInvoiceDiscountDto Dto { get; }

        public AddInvoiceDiscountCommand(Guid invoiceId, CreateInvoiceDiscountDto dto)
        {
            InvoiceId = invoiceId;
            Dto = dto;
        }
    }

    /// <summary>
    /// 9. Add multiple discounts to an invoice
    /// </summary>
    public class AddInvoiceDiscountsCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public IEnumerable<CreateInvoiceDiscountDto> Dtos { get; }

        public AddInvoiceDiscountsCommand(Guid invoiceId, IEnumerable<CreateInvoiceDiscountDto> dtos)
        {
            InvoiceId = invoiceId;
            Dtos = dtos;
        }
    }

    /// <summary>
    /// 10. Update a discount
    /// </summary>
    public class UpdateInvoiceDiscountCommand : IRequest<APIResponseDto>
    {
        public UpdateInvoiceDiscountDto Dto { get; }
        public UpdateInvoiceDiscountCommand(UpdateInvoiceDiscountDto dto) => Dto = dto;
    }

    /// <summary>
    /// 11. Delete a discount
    /// </summary>
    public class DeleteInvoiceDiscountCommand : IRequest<APIResponseDto>
    {
        public Guid DiscountId { get; }
        public DeleteInvoiceDiscountCommand(Guid discountId) => DiscountId = discountId;
    }

    /// <summary>
    /// 12. Delete all discounts of an invoice
    /// </summary>
    public class DeleteAllDiscountsOfInvoiceCommand : IRequest<APIResponseDto>
    {
        public Guid InvoiceId { get; }
        public DeleteAllDiscountsOfInvoiceCommand(Guid invoiceId) => InvoiceId = invoiceId;
    }
}