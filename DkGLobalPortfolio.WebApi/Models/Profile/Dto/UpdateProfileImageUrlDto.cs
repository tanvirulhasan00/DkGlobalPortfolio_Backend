namespace DkGLobalPortfolio.WebApi.Models.Profile.Dto
{
    public class UpdateProfileImageUrlDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public IFormFile ImageUrl { get; set; }
        public int? OwnerId { get; set; }
    }
}
