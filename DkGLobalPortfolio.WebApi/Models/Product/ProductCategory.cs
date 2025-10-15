using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Product
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
