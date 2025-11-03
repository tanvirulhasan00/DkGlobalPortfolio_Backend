namespace DkGLobalPortfolio.WebApi.Models.ClientTestimonialDto.Dto
{
    public class UpdateClientTestimonialDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Message { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public decimal ReviewStars { get; set; }
    }
}
