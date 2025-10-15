using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Profile;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IBranchService : IService<Branch>
    {
        void Update(Branch branch);
    }
}
