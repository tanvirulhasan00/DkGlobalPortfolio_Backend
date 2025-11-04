namespace DkGLobalPortfolio.WebApi.Models.Product.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
