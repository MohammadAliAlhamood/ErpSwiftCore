using ErpSwiftCore.Domain.EntityCompany;
using ErpSwiftCore.Domain.IRepositories;
using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
using ErpSwiftCore.Domain.IRepositories.IUnitOfMeasurementRepositories;
using ErpSwiftCore.Infrastructure.Caching;
using ErpSwiftCore.Infrastructure.Validation;
using ErpSwiftCore.Persistence.Context;
using ErpSwiftCore.Persistence.Repositories.CompanyRepositories;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.TenantManagement.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
namespace ErpSwiftCore.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly IUserProvider _userProvider;
        public ICompanyRepository Company { get; }
        public ICompanyBranchRepository CompanyBranch { get; }
        public ICompanySettingsRepository CompanySettings { get; }
        public ICurrencyRepository Currency { get; }
        public IUnitOfMeasurementRepository UnitOfMeasurement { get; }
        public UnitOfWork(
            AppDbContext db,
            IUserProvider userProvider,
            IIncludeValidator<Company> companyIncludeValidator,
            IIncludeValidator<CompanyBranch> branchIncludeValidator,
            IIncludeValidator<CompanySettings> settingsIncludeValidator,
            IIncludeValidator<Currency> currencyIncludeValidator,
            IIncludeValidator<UnitOfMeasurement> uomIncludeValidator )
        {
            _db = db;
            _userProvider = userProvider;
            Company = new CompanyRepository(_db, _userProvider, companyIncludeValidator);
            CompanyBranch = new CompanyBranchRepository(_db, _userProvider, branchIncludeValidator);
            CompanySettings = new CompanySettingsRepository(_db, _userProvider, settingsIncludeValidator);
            Currency = new CurrencyRepository(_db, _userProvider, currencyIncludeValidator);
            UnitOfMeasurement = new UnitOfMeasurementRepository(_db, _userProvider, uomIncludeValidator);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Database.BeginTransactionAsync(cancellationToken);
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
