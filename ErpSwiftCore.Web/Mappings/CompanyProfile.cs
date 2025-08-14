using AutoMapper;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Companies;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.CompanyBranchs;
using ErpSwiftCore.Web.Models.CompanySystemManagmentModels.Currencies;
using ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels.ValueObjectModels;
namespace ErpSwiftCore.Web.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            // ──────────── خريطة لـ Address و ContactInfo ────────────

            // Mapping من ModifyAddressDto إلى AddressDto
            CreateMap<ModifyAddressDto, AddressDto>();

            // Mapping من AddressDto إلى ModifyAddressDto (لأغراض العرض في شاشة التعديل)
            CreateMap<AddressDto, ModifyAddressDto>();

            // Mapping من ModifyContactInfoDto إلى ContactInfoDto
            CreateMap<ModifyContactInfoDto, ContactInfoDto>();

            // Mapping من ContactInfoDto إلى ModifyContactInfoDto (لأغراض العرض في شاشة التعديل)
            CreateMap<ContactInfoDto, ModifyContactInfoDto>();


            // ──────────── CompanyCreateDto → CompanyDto ────────────
            // عند إنشاء شركة جديدة:
            // - Id يتم تجاهله (لتوليده من الـ backend)
            // - IsActive يتم ضبطها تلقائيًا true
            CreateMap<CompanyCreateDto, CompanyDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                // يطابق الحقل Address (ModifyAddressDto → AddressDto)
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                // يطابق الحقل ContactInfo (ModifyContactInfoDto → ContactInfoDto)
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));


            // ──────────── CompanyUpdateDto → CompanyDto ────────────
            // عند تعديل شركة موجودة:
            // - CompanyCode عادة لا يتم تعديله
            CreateMap<CompanyUpdateDto, CompanyDto>()
                // يطابق الحقل Address (ModifyAddressDto → AddressDto)
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                // يطابق الحقل ContactInfo (ModifyContactInfoDto → ContactInfoDto)
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));


            // ──────────── CompanyDto → CompanyUpdateDto ────────────
            // لعرض بيانات الشركة في شاشة التعديل:
            CreateMap<CompanyDto, CompanyUpdateDto>()
                // AddressDto → ModifyAddressDto
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                // ContactInfoDto → ModifyContactInfoDto
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));


            // ──────────── CompanyDto → CompanyCreateDto ────────────
            // إذا احتجنا تكرار شركة جديدة مع بعض القيم مسبقًا:
            CreateMap<CompanyDto, CompanyCreateDto>()
                // AddressDto → ModifyAddressDto
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                // ContactInfoDto → ModifyContactInfoDto
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => src.ContactInfo));


            // من CompanyBranchCreateDto إلى CompanyBranch (Entity)
            CreateMap<CompanyBranchCreateDto, CompanyBranchDto>().ReverseMap();
            CreateMap<CompanyBranchUpdateDto, CompanyBranchDto>().ReverseMap();

            CreateMap<CurrencyDto, CurrencyCreateDto>().ReverseMap();
            CreateMap<CurrencyDto, CurrencyUpdateDto>().ReverseMap();




        }
    }
}
