namespace DkGLobalPortfolio.WebApi.Models.Profile.Dto
{
    public class CreateCompanyInfoDto
    {
        public string? Name { get; set; }
        public string? Quote { get; set; }
        public string? ShortTitle { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Location { get; set; }

        //links
        public string? MapLink { get; set; }
        public string? SecondMapLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TwitterLink { get; set; }

        //mission,vision
        public string? Mission { get; set; }
        public string? Vision { get; set; }

        //factory
        public decimal AnnualTurnover { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfSewingPlants { get; set; }
        public int ProductionCapacity { get; set; }
        public string? PrimaryMarkets { get; set; }

      
    }
}
