using AutoMapper;
using ErpSwiftCore.Application.Features.Companies.CompanyBranchs.Dtos;
using ErpSwiftCore.SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErpSwiftCore.Application.Features.Companies.CompanyBranchs.MappingProfiles
{
    /// <summary>
    /// يحتوي على قواعد AutoMapper الخاصة بتحويل الكائنات بين الـ Entities والـ DTOs الخاصة بفروع الشركة (CompanyBranch).
    /// </summary>
    public class CompanyBranchMappingProfile : Profile
    {
        public CompanyBranchMappingProfile()
        {
            #region ──────────── CompanyBranch Mappings ────────────

            // من CompanyBranchCreateDto إلى CompanyBranch (Entity)
            CreateMap<CompanyBranchCreateDto, CompanyBranch>(MemberList.None)
                .ForMember(dest => dest.ID, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.BranchCode));

            // من CompanyBranchUpdateDto إلى CompanyBranch (Entity) للـ Update
            CreateMap<CompanyBranchUpdateDto, CompanyBranch>(MemberList.None)
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));

            // من CompanyBranch إلى CompanyBranchDto (لـ Read / Response)
            CreateMap<CompanyBranch, CompanyBranchDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CompanyID, opt => opt.MapFrom(src => src.CompanyID))
                .ForMember(dest => dest.BranchName, opt => opt.MapFrom(src => src.BranchName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo))
                .ForMember(dest => dest.BranchCode, opt => opt.MapFrom(src => src.BranchCode));

            #endregion
        }
    }

}
