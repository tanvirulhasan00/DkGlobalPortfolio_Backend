namespace DkGLobalPortfolio.WebApi.Models.Product.Dto
{
    public class UpdateProductCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
