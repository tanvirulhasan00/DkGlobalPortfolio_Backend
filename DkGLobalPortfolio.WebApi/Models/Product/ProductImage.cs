using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Product
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }

        // Foreign key to Product
        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
