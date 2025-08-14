namespace ErpSwiftCore.Domain.Entities.EntityBilling
{
    public class ImportResult
    {
        public int TotalRows { get; set; }
        public int Successful { get; set; }
        public int Failed { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}
