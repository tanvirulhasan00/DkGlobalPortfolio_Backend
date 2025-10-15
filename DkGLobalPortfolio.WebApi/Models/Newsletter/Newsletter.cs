using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Newsletter
{
    public class Newsletter
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
