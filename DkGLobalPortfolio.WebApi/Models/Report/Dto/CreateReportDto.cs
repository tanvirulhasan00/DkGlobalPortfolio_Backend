namespace DkGLobalPortfolio.WebApi.Models.Report.Dto
{
    public class CreateReportDto
    {
       
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public IFormFile PdfLink { get; set; }
        public int ReportCategoryId { get; set; }
    }
}
