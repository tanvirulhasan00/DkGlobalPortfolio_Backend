namespace DkGLobalPortfolio.WebApi.Models.Report.Dto
{
    public class CreateReportDto
    {
       
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PdfLink { get; set; }
        public int ReportCategoryId { get; set; }
    }
}
