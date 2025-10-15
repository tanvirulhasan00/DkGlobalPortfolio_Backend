using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Product;
using DkGLobalPortfolio.WebApi.Models.Profile;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IProductImageService : IService<ProductImage>
    {
        void Update(ProductImage productImage);
    }
}
