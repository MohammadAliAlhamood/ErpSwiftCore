using AutoMapper;
using ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.Dtos;
using ErpSwiftCore.Domain.Entities.EntityFinancial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Financials.CustomFinancialReportResults.MappingProfiles
{
    public class FinancialReportProfile : Profile
    {
        public FinancialReportProfile()
        {
            // Core entity ↔ DTO
            CreateMap<CustomFinancialReportResult, CustomFinancialReportResultDto>()
                .ReverseMap();

            // Save (create/update)
            CreateMap<CustomFinancialReportResult, SaveReportDto>()
                .ReverseMap();

            // Validation uses same shape as SaveReportDto
            CreateMap<CustomFinancialReportResult, ValidateReportDto>()
                .ReverseMap();

            // Note: SaveReportsRangeDto contains collection of SaveReportDto,
            // so no direct map needed here. The element mapping above suffices.
        }
    }
}







