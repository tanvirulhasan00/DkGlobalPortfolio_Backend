using DkGLobalPortfolio.WebApi.Models.Blog;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IPostService : IService<Post>
    {
        void Update(Post post);
    }
}
