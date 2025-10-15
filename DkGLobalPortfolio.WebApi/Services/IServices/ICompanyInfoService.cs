using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Models.Report;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
   public interface ICompanyInfoService : IService<CompanyInfo>
    {
        void Update(CompanyInfo companyInfo);
    }
}
