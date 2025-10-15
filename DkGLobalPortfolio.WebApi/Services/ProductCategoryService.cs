using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Product;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ProductCategoryService : Service<ProductCategory>, IProductCategoryService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ProductCategoryService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductCategory productCategory)
        {
            _db.Update(productCategory);
        }
    }
}
