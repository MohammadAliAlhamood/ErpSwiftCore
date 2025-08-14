namespace ErpSwiftCore.Web.Models.ProductSystemManagmentModels.ProductUnitConversionModels
{
    public class ProductUnitConversionCreateDto
    {
        public Guid ProductId { get; set; }
        public Guid FromUnitId { get; set; }
        public Guid ToUnitId { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal Factor { get; set; }
    }
}
