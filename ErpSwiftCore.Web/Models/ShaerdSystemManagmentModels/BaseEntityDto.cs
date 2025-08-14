namespace ErpSwiftCore.Web.Models.ShaerdSystemManagmentModels
{
    public class BaseEntityDto
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; } 
    }
}