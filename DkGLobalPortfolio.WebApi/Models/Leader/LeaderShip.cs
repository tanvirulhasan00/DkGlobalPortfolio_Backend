using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Leader
{
    public class LeaderShip
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}
