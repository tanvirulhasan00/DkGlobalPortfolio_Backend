using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Report;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IReportCategoryService : IService<ReportCategory>
    {
        void Update(ReportCategory reportCategory);
    }
}
