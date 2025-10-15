using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Product;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IProductService : IService<Product>
    {
        void Update(Product product);
    }
}
