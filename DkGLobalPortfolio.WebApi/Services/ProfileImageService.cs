using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Blog;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.Profile;
using DkGLobalPortfolio.WebApi.Services.IServices;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ProfileImageService : Service<ProfileImage>, IProfileImageService
    {
        private readonly DkGlobalPortfolioDbContext _db;
        public ProfileImageService(DkGlobalPortfolioDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProfileImage profileImage)
        {
            _db.Update(profileImage);
        }
    }
}
