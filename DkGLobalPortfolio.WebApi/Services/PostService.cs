using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public PostService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Post post)
        {
            _db.Update(post);
        }
    }
}
