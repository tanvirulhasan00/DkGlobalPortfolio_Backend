namespace DkGLobalPortfolio.WebApi.Models.Partner.Dto
{
    public class UpdatePartnerDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
