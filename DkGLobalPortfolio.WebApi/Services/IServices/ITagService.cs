using DkGLobalPortfolio.WebApi.Models.Blog;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface ITagService : IService<Tag>
    {
        void Update(Tag tag);
    }
}
