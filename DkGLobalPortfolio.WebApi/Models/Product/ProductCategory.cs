using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Product
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public string? Link { get; set; } 
        public string? Icon { get; set; } 
        public bool IsActive { get; set; }

    }
}
