using Microsoft.AspNetCore.Identity;

namespace DkGLobalPortfolio.WebApi.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
