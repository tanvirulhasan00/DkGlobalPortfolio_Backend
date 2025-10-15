using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Profile
{
    public class ProfileImage
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // Foreign key to Info
        public int? CompanyInfoId { get; set; }
        [ForeignKey("CompanyInfoId")]
        public CompanyInfo? CompanyInfo { get; set; }
    }
}
