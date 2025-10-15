using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Product
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public bool IsActive { get; set; }

        public int ProductCategoryId { get; set; }
        [ForeignKey("ProductCategoryId")]
        public ProductCategory ProductCategory { get; set; }

        // Navigation property for multiple images
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
