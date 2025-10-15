using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Profile;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IBankInfoService : IService<BankInfo>
    {
        void Update(BankInfo bankInfo);
    }
}
