namespace DkGLobalPortfolio.WebApi.Models.Product.Dto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
