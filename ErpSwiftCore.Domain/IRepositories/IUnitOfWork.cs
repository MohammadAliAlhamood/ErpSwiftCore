using ErpSwiftCore.Domain.IRepositories.ICompanyRepositories;
using ErpSwiftCore.Domain.IRepositories.IUnitOfMeasurementRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ErpSwiftCore.Domain.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Company { get; }
        ICompanyBranchRepository CompanyBranch { get; }
        ICompanySettingsRepository CompanySettings { get; }
        ICurrencyRepository Currency { get; }
        IUnitOfMeasurementRepository UnitOfMeasurement { get; }
        Task SaveAsync();

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}