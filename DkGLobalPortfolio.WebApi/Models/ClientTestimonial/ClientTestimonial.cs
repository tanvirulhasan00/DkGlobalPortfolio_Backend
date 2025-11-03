using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.ClientTestimonial
{
    public class ClientTestimonial
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public decimal ReviewStars { get; set; }
        public bool IsActive { get; set; }
    }
}
