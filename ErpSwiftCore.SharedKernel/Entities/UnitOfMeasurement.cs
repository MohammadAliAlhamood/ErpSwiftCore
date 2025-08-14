using ErpSwiftCore.SharedKernel.Base;
namespace ErpSwiftCore.SharedKernel.Entities
{
    public class UnitOfMeasurement : BaseEntity
    { 
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public string? Description { get; set; }
    }
}