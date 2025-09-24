using DkGLobalPortfolio.WebApi.Models.Blog;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IBlogPostTagService : IService<BlogPostTag>
    {
        void Update(BlogPostTag blogPostTag);
    }
}
