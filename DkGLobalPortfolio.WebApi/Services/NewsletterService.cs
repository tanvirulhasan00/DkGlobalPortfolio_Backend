using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Newsletter;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class NewsletterService : Service<Newsletter>, INewsletterService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public NewsletterService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Newsletter newsletter)
        {
            _db.Update(newsletter);
        }
    }
}
