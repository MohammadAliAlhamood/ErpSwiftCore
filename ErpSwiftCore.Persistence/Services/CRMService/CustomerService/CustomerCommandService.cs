using ErpSwiftCore.Domain.Entities.EntityCRM;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IServices.ICRMService.ICustomerService;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ErpSwiftCore.Persistence.Services.CRMService.CustomerService
{
    public class CustomerCommandService : ICustomerCommandService
    {
        private readonly IMultiTenantUnitOfWork _unitOfWork;
        private readonly ICustomerValidationService _validationService;

        public CustomerCommandService(
            IMultiTenantUnitOfWork unitOfWork,
            ICustomerValidationService validationService)
        {
            _unitOfWork = unitOfWork;
            _validationService = validationService;
        }

        // ----------- [Create / Bulk Create] -----------

        public async Task<Guid> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            // Required fields validation
            if (string.IsNullOrWhiteSpace(customer.CustomerCode))
                throw new InvalidOperationException("CustomerCode is required.");
            if (string.IsNullOrWhiteSpace(customer.NationalID))
                throw new InvalidOperationException("NationalID is required.");

            // Uniqueness checks via validation service
            bool isUnique = await _validationService.CustomerIsUniqueAsync(
                customer.CustomerCode,
                customer.ContactInfo?.Email,
                customer.NationalID,
                customer.ContactInfo?.Phone,
                cancellationToken);

            if (!isUnique)
                throw new InvalidOperationException("One or more customer identifiers already exist.");

            // Set audit fields (if any)...

            var id = await _unitOfWork.Customer.CreateAsync(customer, cancellationToken);
            await _unitOfWork.SaveAsync();
            return id;
        }

        // ----------- [Update] -----------

        public async Task<bool> UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            // Ensure exists
            if (!await _validationService.CustomerExistsAsync(customer.ID, cancellationToken))
                return false;

            var existing = await _unitOfWork.Customer.GetByIdAsync(customer.ID, cancellationToken);
            if (existing == null)
                return false;

            // Uniqueness checks for update
            bool isUnique = await _validationService.CustomerIsUniqueAsync(
                customer.ID,
                customer.CustomerCode,
                customer.ContactInfo?.Email,
                customer.NationalID,
                customer.ContactInfo?.Phone,
                cancellationToken);

            if (!isUnique)
                throw new InvalidOperationException("One or more customer identifiers already exist.");

            // Map updatable fields
            existing.FirstName = customer.FirstName;
            existing.MiddleName = customer.MiddleName;
            existing.LastName = customer.LastName;
            existing.Gender = customer.Gender;
            existing.NationalID = customer.NationalID;
            existing.Address = customer.Address;
            existing.ContactInfo = customer.ContactInfo;
            existing.Notes = customer.Notes;
            existing.CustomerCode = customer.CustomerCode;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = customer.UpdatedBy;

            var result = await _unitOfWork.Customer.UpdateAsync(existing, cancellationToken);
            if (result)
                await _unitOfWork.SaveAsync();

            return result;
        }

        public async Task<IEnumerable<Guid>> AddCustomersAsync(IEnumerable<Customer> customers, CancellationToken cancellationToken = default)
        {
            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            var list = customers.ToList();
            var ids = new List<Guid>();

            using (IDbContextTransaction tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    foreach (var customer in list)
                    {
                        if (string.IsNullOrWhiteSpace(customer.CustomerCode))
                            throw new InvalidOperationException("CustomerCode is required.");
                        if (string.IsNullOrWhiteSpace(customer.NationalID))
                            throw new InvalidOperationException("NationalID is required.");

                        bool isUnique = await _validationService.CustomerIsUniqueAsync(
                            customer.CustomerCode,
                            customer.ContactInfo?.Email,
                            customer.NationalID,
                            customer.ContactInfo?.Phone,
                            cancellationToken);

                        if (!isUnique)
                            throw new InvalidOperationException("One or more customer identifiers already exist.");

                        customer.CreatedAt = DateTime.UtcNow;
                        var id = await _unitOfWork.Customer.CreateAsync(customer, cancellationToken);
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

        // ----------- [Delete / Soft-Delete / Restore] -----------

        public async Task<bool> DeleteCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.CustomerExistsAsync(customerId, cancellationToken))
                return false;

            bool success = await _unitOfWork.Customer.DeleteAsync(customerId, cancellationToken);
            if (success)
                await _unitOfWork.SaveAsync();
            return success;
        }

        public async Task<bool> DeleteCustomersRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default)
        {
            if (customerIds == null)
                return false;

            var list = customerIds.ToList();
            using (var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    bool allDeleted = true;
                    foreach (var id in list)
                    {
                        if (!await DeleteCustomerAsync(id, cancellationToken))
                            allDeleted = false;
                    }

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

        public async Task<bool> DeleteAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            var allActive = await _unitOfWork.Customer.GetAllAsync(null, cancellationToken);
            return await DeleteCustomersRangeAsync(allActive.Select(c => c.ID), cancellationToken);
        }

        public async Task<bool> RestoreCustomerAsync(Guid customerId, CancellationToken cancellationToken = default)
        {
            if (!await _validationService.CustomerExistsAsync(customerId, cancellationToken))
                return false;

            bool success = await _unitOfWork.Customer.RestoreAsync(customerId, cancellationToken);
            if (success)
                await _unitOfWork.SaveAsync();
            return success;
        }

        public async Task<bool> RestoreCustomersRangeAsync(IEnumerable<Guid> customerIds, CancellationToken cancellationToken = default)
        {
            if (customerIds == null)
                return false;

            var list = customerIds.ToList();
            using (var tx = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    bool allRestored = true;
                    foreach (var id in list)
                    {
                        if (!await RestoreCustomerAsync(id, cancellationToken))
                            allRestored = false;
                    }

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

        public async Task<bool> RestoreAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            var allSoftDeleted = await _unitOfWork.Customer.GetAllSoftDeletedAsync(null, cancellationToken);
            return await RestoreCustomersRangeAsync(allSoftDeleted.Select(c => c.ID), cancellationToken);
        }
    }
}
