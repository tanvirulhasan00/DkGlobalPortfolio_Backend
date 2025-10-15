using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class BankInfoService : Service<BankInfo>, IBankInfoService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public BankInfoService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BankInfo bankInfo)
        {
            _db.Update(bankInfo);
        }
    }
}
