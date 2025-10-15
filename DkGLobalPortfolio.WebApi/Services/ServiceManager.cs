using DkGLobalPortfolio.WebApi.Database;
using DkGLobalPortfolio.WebApi.Models.Partner;
using DkGLobalPortfolio.WebApi.Models.User;
using DkGLobalPortfolio.WebApi.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace DkGLobalPortfolio.WebApi.Services
{
    public class ServiceManager : IServiceManager
    {
        public IPostService Posts { get; private set; }
        public IBlogPostTagService BlogPostTags { get; private set; }
        public IBlogCategoryService BlogCategories { get; private set; }
        public ITagService Tags { get; private set; }
        public IAuthorService Authors { get; private set; }
        public ILeadershipService Leaderships { get; private set; }
        public IMessageService Messages { get; private set; }
        public INewsletterService Newsletters { get; private set; }
        public IFileService File { get; private set; }
        public IPartnerService Partners { get; private set; }
        public IReportCategoryService ReportCategories { get; private set; }
        public IReportService Reports { get; private set; }
        public IAuthService Auth { get; private set; }
        public IBranchService Branches { get; private set; }
        public ICompanyInfoService CompanyInfos { get; private set; }
        public IBankInfoService BankInfos { get; private set; }
        public IProfileImageService ProfileImages { get; private set; }
        public IProductImageService ProductImages { get; private set; }
        public IProductService Products { get; private set; }
        public IProductCategoryService ProductCategories { get; private set; }
        
        //break point
        private readonly DkGlobalPortfolioDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly string _secretKey;
        public ServiceManager(DkGlobalPortfolioDbContext db,IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _db = db;
            _env = env;
            _secretKey = configuration["TokenSetting:SecretKey"] ?? "";
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            Posts = new PostService(_db);
            BlogPostTags = new BlogPostTagService(_db);
            BlogCategories = new BlogCategoryService(_db);
            Tags = new TagService(_db);
            Authors = new AuthorService(_db);
            Leaderships = new LeadershipService(_db);
            Messages = new MessageService(_db);
            Newsletters = new NewsletterService(_db);
            File = new FileService(_env,_httpContextAccessor);
            Partners = new PartnerService(_db);
            ReportCategories = new ReportCategoryService(_db);
            Reports = new ReportService(_db);
            Auth = new AuthService(_db,_userManager,_roleManager,_secretKey);
            Branches = new BranchService(_db);
            CompanyInfos = new CompanyInfoService(_db);
            BankInfos = new BankInfoService(_db);
            ProfileImages = new ProfileImageService(_db);
            ProductImages = new ProductImageService(_db);
            Products = new ProductService(_db);
            ProductCategories = new ProductCategoryService(_db);
        }
        public async Task<int> Save()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
