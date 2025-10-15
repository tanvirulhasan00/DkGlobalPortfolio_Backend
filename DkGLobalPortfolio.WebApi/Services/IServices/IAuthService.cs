using DkGLobalPortfolio.WebApi.Models.Response;
using DkGLobalPortfolio.WebApi.Models.User;
using DkGLobalPortfolio.WebApi.Models.User.Dto;

namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IAuthService : IService<ApplicationUser>
    {
        Task<ApiResponse> Login(string username, string password);
        Task<ApiResponse> Registration(CreateApplicationUserDto req);
        void Update(ApplicationUser user);
    }
}
