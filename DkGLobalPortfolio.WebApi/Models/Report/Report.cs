using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DkGLobalPortfolio.WebApi.Models.Report
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public string? PdfLink { get; set; }
        public bool IsActive { get; set; }

        public int ReportCategoryId { get; set; }
        [ForeignKey("ReportCategoryId")]
        public ReportCategory? ReportCategory { get; set; }
    }
}
