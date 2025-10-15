using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Profile
{
    public class FinancialInfo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Scale { get; set; }
    }
}
