using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Profile
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string MapLink { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public int CompanyInfoId { get; set; }
        [ForeignKey("CompanyInfoId")]
        public CompanyInfo CompanyInfo { get; set; }

    }
}
