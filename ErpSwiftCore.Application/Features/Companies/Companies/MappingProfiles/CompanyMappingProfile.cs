using AutoMapper;
using ErpSwiftCore.Application.Dtos.ValueObjectDto;
using ErpSwiftCore.Application.Features.Companies.Companies.Dtos;
using ErpSwiftCore.SharedKernel.Entities;
using ErpSwiftCore.SharedKernel.Enums;
using ErpSwiftCore.SharedKernel.ValueObjects;

namespace ErpSwiftCore.Application.Features.Companies.Companies.MappingProfiles
{
    /// <summary>
    /// AutoMapper profile for Company entity and related DTOs, including nested Address and ContactInfo VOs.
    /// </summary>
    public class CompanyMappingProfile : Profile
    {
        public CompanyMappingProfile()
        {
            // ───────── Address ValueObject Mappings ─────────
            CreateMap<AddressDto, Address>()
                .ConstructUsing(src => new Address(
                    src.Street ?? string.Empty,
                    src.City ?? string.Empty,
                    src.State ?? string.Empty,
                    src.PostalCode ?? string.Empty,
                    src.Country ?? string.Empty
                ));
            CreateMap<Address, AddressDto>();

            // Mapping لـ ModifyAddressDto → Address
            CreateMap<ModifyAddressDto, Address>()
                .ConstructUsing(src => new Address(
                    src.Street ?? string.Empty,
                    src.City ?? string.Empty,
                    src.State ?? string.Empty,
                    src.PostalCode ?? string.Empty,
                    src.Country ?? string.Empty
                ));

            // ──────── ContactInfo ValueObject Mappings ───────
            CreateMap<ContactInfoDto, Contact>()
                .ConstructUsing(src => new Contact
                {
                    Email = src.Email ?? string.Empty,
                    Phone = src.Phone ?? string.Empty,
                    Mobile = src.Mobile ?? string.Empty,
                    Fax = src.Fax ?? string.Empty,
                    Website = src.Website ?? string.Empty,
                    Other = src.Other ?? string.Empty
                });
            CreateMap<Contact, ContactInfoDto>();

            // Mapping لـ ModifyContactInfoDto → Contact
            CreateMap<ModifyContactInfoDto, Contact>()
                .ConstructUsing(src => new Contact
                {
                    Email = src.Email ?? string.Empty,
                    Phone = src.PhoneNumber ?? string.Empty,
                    Mobile = src.Mobile ?? string.Empty,
                    Fax = src.Fax ?? string.Empty,
                    Website = src.Website ?? string.Empty,
                    Other = src.Other ?? string.Empty
                });

            // ───────── Modify DTOs Reverse Mappings ─────────
            CreateMap<AddressDto, ModifyAddressDto>();
            CreateMap<ContactInfoDto, ModifyContactInfoDto>();

            // ───────── CompanyCreateDto → Company ─────────
            CreateMap<CompanyCreateDto, Company>()
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));

            // ───────── CompanyUpdateDto → Company ─────────
            CreateMap<CompanyUpdateDto, Company>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));

            // ───────── Company → CompanyDto (Read) ─────────
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            // ───────── Reverse mappings for DTO population ─────────
            CreateMap<CompanyDto, CompanyUpdateDto>();
            CreateMap<CompanyDto, CompanyCreateDto>() ;
        }
    }
}