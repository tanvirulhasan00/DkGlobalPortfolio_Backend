using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IPartnerService : IService<Partner>
    {
        void Update(Partner partner);
    }
}
