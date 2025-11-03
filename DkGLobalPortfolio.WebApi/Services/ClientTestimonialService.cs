using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.ClientTestimonial;
using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ClientTestimonialService : Service<ClientTestimonial>, IClientTestimonialService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ClientTestimonialService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClientTestimonial clientTestimonial)
        {
            _db.Update(clientTestimonial);
        }
    }
}
