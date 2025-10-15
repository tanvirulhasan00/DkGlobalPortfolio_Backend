using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class PartnerService : Service<Partner>, IPartnerService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public PartnerService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Partner partner)
        {
            _db.Update(partner);
        }
    }
}
