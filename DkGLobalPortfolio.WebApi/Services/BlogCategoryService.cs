using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class BlogCategoryService : Service<Category>, IBlogCategoryService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public BlogCategoryService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Update(category);
        }
    }
}
