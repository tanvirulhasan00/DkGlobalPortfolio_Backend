using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class AuthorService : Service<Author>, IAuthorService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public AuthorService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Author author)
        {
            _db.Update(author);
        }
    }
}
