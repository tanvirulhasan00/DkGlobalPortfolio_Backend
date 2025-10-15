namespace DkGLobalPortfolio.WebApi.Models.Profile.Dto
{
    public class CreateProfileImageUrlDto
    {
        public string? Title { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int? OwnerId { get; set; }
    }
}
