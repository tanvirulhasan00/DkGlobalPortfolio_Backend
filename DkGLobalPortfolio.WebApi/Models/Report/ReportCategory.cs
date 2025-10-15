using System.ComponentModel.DataAnnotations;

namespace DkGLobalPortfolio.WebApi.Models.Report
{
    public class ReportCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
