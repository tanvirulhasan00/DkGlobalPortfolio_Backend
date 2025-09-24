using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class BlogPostTagService : Service<BlogPostTag>, IBlogPostTagService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public BlogPostTagService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(BlogPostTag blogPostTag)
        {
            _db.Update(blogPostTag);
        }
    }
}
