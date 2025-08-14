using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.Currencies.Dtos;
using ErpSwiftCore.SharedKernel.Entities;
namespace ErpSwiftCore.Application.Features.Companies.Currencies.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for Currency entity and related DTOs.
    /// </summary>
    public class CurrencyMappingProfile : Profile
    {
        public CurrencyMappingProfile()
        {
            // ───────── CurrencyCreateDto ↔ Currency ─────────
            CreateMap<CurrencyCreateDto, Currency>().ReverseMap();

            // ───────── CurrencyUpdateDto ↔ Currency ─────────
            CreateMap<CurrencyUpdateDto, Currency>().ReverseMap();

            // ───────── Currency ↔ CurrencyDto (Read) ─────────
            CreateMap<Currency, CurrencyDto>().ReverseMap();

            // ───────── Additional reverse mappings (if needed) ─────────
            CreateMap<CurrencyDto, CurrencyCreateDto>().ReverseMap();
            CreateMap<CurrencyDto, CurrencyUpdateDto>().ReverseMap();
        }
    }
}