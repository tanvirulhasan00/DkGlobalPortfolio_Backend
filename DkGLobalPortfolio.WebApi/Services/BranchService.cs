using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class BranchService : Service<Branch>, IBranchService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public BranchService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Branch branch)
        {
            _db.Update(branch);
        }
    }
}
