using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class LeadershipService : Service<LeaderShip>, ILeadershipService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public LeadershipService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LeaderShip ship)
        {
            _db.Update(ship);
        }
    }
}
