using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ServiceManager : IServiceManager
    {
        public IPostService Posts { get; private set; }
        public IBlogPostTagService BlogPostTags { get; private set; }
        //brake point
        private readonly DkGlobalPortfolioDbContext _db;
        public ServiceManager(DkGlobalPortfolioDbContext db)
        {
            _db = db;
            Posts = new PostService(db);
            BlogPostTags = new BlogPostTagService(db);
        }
        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
