namespace DkGLobalPortfolio.WebApi.Models.Partner.Dto
{
    public class CreatePartnerDto
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
