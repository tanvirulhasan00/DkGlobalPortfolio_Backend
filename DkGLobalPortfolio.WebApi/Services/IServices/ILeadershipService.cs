using DkGLobalPortfolio.WebApi.Models.Leader;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface ILeadershipService : IService<LeaderShip>
    {
        void Update(LeaderShip ship);
    }
}
