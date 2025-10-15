using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Product;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ProductImageService : Service<ProductImage>, IProductImageService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ProductImageService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductImage productImage)
        {
            _db.Update(productImage);
        }
    }
}
