using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Product;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        void Update(ProductCategory productCategory);
    }
}
