using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class TagService : Service<Tag>, ITagService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public TagService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Tag tag)
        {
            _db.Update(tag);
        }
    }
}
