using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Report;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class CompanyInfoService : Service<CompanyInfo>, ICompanyInfoService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public CompanyInfoService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CompanyInfo companyInfo)
        {
            _db.Update(companyInfo);
        }
    }
}
