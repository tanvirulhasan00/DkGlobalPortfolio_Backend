using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Report;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ReportService : Service<Report>, IReportService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ReportService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Report report)
        {
            _db.Update(report);
        }
    }
}
