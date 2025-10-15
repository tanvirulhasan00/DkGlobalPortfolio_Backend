namespace DkGLobalPortfolio.WebApi.Models.Leader.Dto
{
    public class CreateLeadershipDto
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile ImageUrl { get; set; }
    }
}
