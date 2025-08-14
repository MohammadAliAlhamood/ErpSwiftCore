using ErpSwiftCore.Domain.Entities.EntityBilling;
using ErpSwiftCore.Domain.Enums;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.IBillingService.IOrderService;
using System.Text.Json;
namespace ErpSwiftCore.Persistence.Services.BillingService.OrderService
{
    public class OrderCommandService : IOrderCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly IOrderValidationService _validation;
        public OrderCommandService(IMultiTenantUnitOfWork unitOfWork, IOrderValidationService validation)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _validation = validation ?? throw new ArgumentNullException(nameof(validation));
        }
        public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (order.OrderLines == null || !order.OrderLines.Any())
                throw new InvalidOperationException("Order must have at least one OrderLine.");
            var party = await _unitOfWork.Party.GetByIdAsync(order.PartyId, cancellationToken) ?? throw new InvalidOperationException("Party not found.");
            order.Party = party;
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // إنشاء رأس الطلب
                order.ID = await _unitOfWork.Order.CreateAsync(order, cancellationToken);
                foreach (var line in order.OrderLines)
                {
                    line.OrderId = order.ID;
                    line.ID = await _unitOfWork.OrderLine.CreateAsync(line, cancellationToken);
                }

                // حفظ


                // إذا مكتمل من البداية، أنشئ فاتورة
                if (order.OrderStatus == OrderStatus.Completed)
                    await CreateInvoiceForOrderAsync(order, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return order;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }


        public async Task<Order> CreateOrderWithLinesAsync(
       Order order,
       IEnumerable<OrderLine> orderLines,
       CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (orderLines == null || !orderLines.Any())
                throw new InvalidOperationException("Order must have at least one OrderLine.");

            // 1) بناء كيان الـ Party بناءً على النوع (Customer أو Supplier) مع تعبئة كل الحقول
            Party newParty = order.Party.Type switch
            {
                PartyType.Customer => new Party
                {
                    // من AuditableEntity
                    // ID سيُملأ بعد الإنشاء في قاعدة البيانات
                    Name = order.Party.Name,
                    Type = PartyType.Customer,

                    // حقول Contact من Party نفسه
                    Email = order.Party.Email,
                    Phone = order.Party.Phone,
                    TaxNumber = order.Party.TaxNumber,
                    Address = order.Party.Address,

                    // ربط بكيان Customer قائم
                    CustomerId = order.Party.CustomerId
                                 ?? throw new InvalidOperationException("CustomerId must be provided for a Customer party."),
                    SupplierId = null,

                    // المجموعات الافتراضية
                    Orders = new List<Order>(),
                    Invoices = new List<Invoice>()
                },

                PartyType.Supplier => new Party
                {
                    Name = order.Party.Name,
                    Type = PartyType.Supplier,

                    Email = order.Party.Email,
                    Phone = order.Party.Phone,
                    TaxNumber = order.Party.TaxNumber,
                    Address = order.Party.Address,

                    CustomerId = null,
                    SupplierId = order.Party.SupplierId
                                 ?? throw new InvalidOperationException("SupplierId must be provided for a Supplier party."),

                    Orders = new List<Order>(),
                    Invoices = new List<Invoice>()
                },

                _ => throw new InvalidOperationException($"Unsupported PartyType: {order.Party.Type}")
            };

            // 2) إنشاء الـ Party في قاعدة البيانات
            newParty.ID = await _unitOfWork.Party.CreateAsync(newParty, cancellationToken);

            // 3) ربط الـ Order بالـ Party


            // 2) إنشاء الـ Party في قاعدة البيانات
            newParty.ID = await _unitOfWork.Party.CreateAsync(newParty, cancellationToken);

            // 3) ربط الـ Order بالـ Party الجديدة
            order.PartyId = newParty.ID;
            order.Party = newParty;

            // 4) بدء المعاملة وإنشاء الطلب مع سطوره
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                // إنشاء رأس الطلب
                order.ID = await _unitOfWork.Order.CreateAsync(order, cancellationToken);

                // إنشاء سطور الطلب وربطها
                foreach (var line in orderLines)
                {
                    line.OrderId = order.ID;
                    line.ID = await _unitOfWork.OrderLine.CreateAsync(line, cancellationToken);
                }

                // 5) إذا الحالة مكتملة، أنشئ فاتورة
                if (order.OrderStatus == OrderStatus.Completed)
                    await CreateInvoiceForOrderAsync(order, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return order;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }


        // -------------------- [Update with Lines] --------------------
        public async Task<Order> UpdateOrderWithLinesAsync(Order order,

            IEnumerable<OrderLine>? linesToAdd = null,
                                                           IEnumerable<OrderLine>? linesToUpdate = null,
                                                           IEnumerable<Guid>? lineIdsToDelete = null,
                                                           CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (!await _validation.CanModifyOrderAsync(order.ID, cancellationToken))
                throw new InvalidOperationException("Cannot modify order: finalized or invoiced.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingOrder = await _unitOfWork.Order.GetByIdAsync(order.ID, cancellationToken)
                                     ?? throw new InvalidOperationException("Order not found.");
                var existingLines = await _unitOfWork.OrderLine.GetByOrderAsync(order.ID, cancellationToken);
                var oldJson = JsonSerializer.Serialize(new { Order = existingOrder, Lines = existingLines });

                // تحديث الرأس
                await _unitOfWork.Order.UpdateAsync(order, cancellationToken);

                // حذف سطور محددة
                if (lineIdsToDelete != null)
                {
                    foreach (var id in lineIdsToDelete)
                        await _unitOfWork.OrderLine.DeleteAsync(id, cancellationToken);
                }

                // تحديث سطور
                if (linesToUpdate != null)
                {
                    foreach (var line in linesToUpdate)
                        await _unitOfWork.OrderLine.UpdateAsync(line, cancellationToken);
                }

                // إضافة سطور جديدة
                if (linesToAdd != null)
                {
                    foreach (var line in linesToAdd)
                    {
                        line.OrderId = order.ID;
                        await _unitOfWork.OrderLine.CreateAsync(line, cancellationToken);
                    }
                }


                await tx.CommitAsync(cancellationToken);
                return order;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OrderLine> AddOrderLineAsync(Guid orderId, OrderLine orderLine, CancellationToken cancellationToken = default)
        {
            if (orderLine == null)
                throw new ArgumentNullException(nameof(orderLine));
            if (!await _validation.CanModifyOrderAsync(orderId, cancellationToken))
                throw new InvalidOperationException("Cannot add line: order is finalized or has an invoice.");

            orderLine.OrderId = orderId;
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                orderLine.ID = await _unitOfWork.OrderLine.CreateAsync(orderLine, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return orderLine;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<IEnumerable<OrderLine>> AddOrderLinesAsync(Guid orderId, IEnumerable<OrderLine> orderLines, CancellationToken cancellationToken = default)
        {
            if (orderLines == null)
                throw new ArgumentNullException(nameof(orderLines));
            if (!await _validation.CanModifyOrderAsync(orderId, cancellationToken))
                throw new InvalidOperationException("Cannot add lines: order is finalized or has an invoice.");

            var created = new List<OrderLine>();
            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                foreach (var line in orderLines)
                {
                    line.OrderId = orderId;
                    line.ID = await _unitOfWork.OrderLine.CreateAsync(line, cancellationToken);
                    created.Add(line);
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



        public async Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            if (!await _validation.CanModifyOrderAsync(order.ID, cancellationToken))
                throw new InvalidOperationException("Cannot modify order: finalized or invoiced.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.Order.GetByIdAsync(order.ID, cancellationToken)
                                ?? throw new InvalidOperationException("Order not found.");
                var oldJson = JsonSerializer.Serialize(existing);

                await _unitOfWork.Order.UpdateAsync(order, cancellationToken);


                await tx.CommitAsync(cancellationToken);
                return order;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<OrderLine> UpdateOrderLineAsync(OrderLine orderLine, CancellationToken cancellationToken = default)
        {
            if (orderLine == null)
                throw new ArgumentNullException(nameof(orderLine));
            if (!await _validation.CanModifyOrderAsync(orderLine.OrderId, cancellationToken))
                throw new InvalidOperationException("Cannot modify line: order is finalized or invoiced.");

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existing = await _unitOfWork.OrderLine.GetByIdAsync(orderLine.ID, cancellationToken)
                                ?? throw new InvalidOperationException("OrderLine not found.");
                var oldJson = JsonSerializer.Serialize(existing);

                await _unitOfWork.OrderLine.UpdateAsync(orderLine, cancellationToken);


                await tx.CommitAsync(cancellationToken);
                return orderLine;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.CanDeleteOrderAsync(orderId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingLines = await _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);
                foreach (var line in existingLines)
                    await _unitOfWork.OrderLine.DeleteAsync(line.ID, cancellationToken);

                var result = await _unitOfWork.Order.DeleteAsync(orderId, cancellationToken);


                await tx.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteOrdersRangeAsync(IEnumerable<Guid> orderIds, CancellationToken cancellationToken = default)
        {
            if (orderIds == null)
                throw new ArgumentNullException(nameof(orderIds));

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var anyDeleted = false;
                foreach (var id in orderIds)
                {
                    if (!await _validation.CanDeleteOrderAsync(id, cancellationToken))
                        continue;

                    var existingLines = await _unitOfWork.OrderLine.GetByOrderAsync(id, cancellationToken);
                    foreach (var line in existingLines)
                        await _unitOfWork.OrderLine.DeleteAsync(line.ID, cancellationToken);

                    var success = await _unitOfWork.Order.DeleteAsync(id, cancellationToken);
                    anyDeleted |= success;
                }


                await tx.CommitAsync(cancellationToken);
                return anyDeleted;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteOrderLineAsync(Guid orderLineId, CancellationToken cancellationToken = default)
        {
            var line = await _unitOfWork.OrderLine.GetByIdAsync(orderLineId, cancellationToken);
            if (line == null || !await _validation.CanModifyOrderAsync(line.OrderId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var result = await _unitOfWork.OrderLine.DeleteAsync(orderLineId, cancellationToken);


                await tx.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> DeleteAllLinesOfOrderAsync(Guid orderId, CancellationToken cancellationToken = default)
        {
            if (!await _validation.CanModifyOrderAsync(orderId, cancellationToken))
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var lines = await _unitOfWork.OrderLine.GetByOrderAsync(orderId, cancellationToken);
                foreach (var line in lines)
                    await _unitOfWork.OrderLine.DeleteAsync(line.ID, cancellationToken);



                await tx.CommitAsync(cancellationToken);
                return true;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<bool> ChangeOrderStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default)
        {
            var order = await _unitOfWork.Order.GetByIdAsync(orderId, cancellationToken);
            if (order == null)
                return false;

            var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                order.OrderStatus = newStatus;
                var updated = await _unitOfWork.Order.UpdateAsync(order, cancellationToken);


                if (newStatus == OrderStatus.Completed)
                    await CreateInvoiceForOrderAsync(order, cancellationToken);

                await tx.CommitAsync(cancellationToken);
                return updated;
            }
            catch
            {
                await tx.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task CreateInvoiceForOrderAsync(Order order, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Invoice.AnyForOrderAsync(order.ID, cancellationToken))
                return;

            var party = order.Party ?? await _unitOfWork.Party.GetByIdAsync(order.PartyId, cancellationToken);

            var invoice = new Invoice
            {
                OrderId = order.ID,
                InvoiceDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(30),
                TotalAmount = order.OrderLines.Sum(l => l.SubTotal),
                PaidAmount = 0m,
                InvoiceStatus = InvoiceStatus.Draft,
                IsFinalized = false,
                InvoiceType = party.Type == PartyType.Customer
                                ? InvoiceType.Sales
                                : InvoiceType.Purchase,
                CurrencyId = order.CurrencyId
            };

            await _unitOfWork.Invoice.CreateAsync(invoice, cancellationToken);

        }
    }
}
