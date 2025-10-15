using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Report;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IReportService : IService<Report>
    {
        void Update(Report report);
    }
}
