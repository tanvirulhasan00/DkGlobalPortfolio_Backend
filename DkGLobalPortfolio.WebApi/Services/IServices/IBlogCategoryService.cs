using DkGLobalPortfolio.WebApi.Models.Blog;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IBlogCategoryService : IService<Category>
    {
        void Update(Category category);
    }
}
