namespace DkGLobalPortfolio.WebApi.Models.Message.Dto
{
    public class UpdateMessageDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string Content { get; set; }
    }
}
