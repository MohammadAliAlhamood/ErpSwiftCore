using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Domain.IServices.ICompanyServices.ICurrencyService
{
    public interface ICurrencyValidationService
    {
        Task<bool> IsCurrencyCodeUniqueAsync(string code, Guid iD, CancellationToken ct);
        Task<bool> IsCurrencyCodeUniqueAsync(string code, CancellationToken ct);
    }
}
