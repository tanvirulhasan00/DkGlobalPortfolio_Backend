using DkGLobalPortfolio.WebApi.Models.Blog;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IAuthorService : IService<Author>
    {
        void Update(Author author);
    }
}
