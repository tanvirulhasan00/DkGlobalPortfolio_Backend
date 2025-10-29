namespace DkGLobalPortfolio.WebApi.Models.Product.Dto
{
    public class UpdateProductCategoryDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public string? Link { get; set; }
        public string? Icon { get; set; }

    }
}
