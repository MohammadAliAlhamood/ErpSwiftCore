namespace ErpSwiftCore.SharedKernel.Base
{
    public class BaseEntity
    {
        public Guid ID { get; set; }  = Guid.NewGuid();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}