using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Report;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ReportCategoryService : Service<ReportCategory>, IReportCategoryService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ReportCategoryService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ReportCategory reportCategory)
        {
            _db.Update(reportCategory);
        }
    }
}
