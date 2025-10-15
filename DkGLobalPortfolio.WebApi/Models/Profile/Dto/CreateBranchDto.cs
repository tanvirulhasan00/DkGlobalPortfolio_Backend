namespace DkGLobalPortfolio.WebApi.Models.Profile.Dto
{
    public class CreateBranchDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string MapLink { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int CompanyInfoId { get; set; }
    }
}
