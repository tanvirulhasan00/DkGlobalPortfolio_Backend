using DkGLobalPortfolio.WebApi.Models.Leader;
using DkGLobalPortfolio.WebApi.Models.Newsletter;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface INewsletterService : IService<Newsletter>
    {
        void Update(Newsletter newsletter);
    }
}
