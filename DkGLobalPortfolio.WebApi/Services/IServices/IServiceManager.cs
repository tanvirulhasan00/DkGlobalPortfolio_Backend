namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IServiceManager
    {
        Task<int> Save();
       public IPostService Posts { get; }
       public IBlogPostTagService BlogPostTags { get; }
       public IBlogCategoryService BlogCategories { get; }
       public ITagService Tags { get; }
       public IAuthorService Authors { get; }
       public ILeadershipService Leaderships { get; }
       public IMessageService Messages { get; }
       public INewsletterService Newsletters { get; }
       public IFileService File { get; }
       public IPartnerService Partners { get; }
       public IReportCategoryService ReportCategories { get; }
       public IReportService Reports { get; }
       public IAuthService Auth { get; }
       public IBranchService Branches { get; }
       public ICompanyInfoService CompanyInfos { get; }
       public IBankInfoService BankInfos { get; }
       public IProfileImageService ProfileImages { get; }
       public IProductImageService ProductImages { get; }
       public IProductService Products { get; }
       public IProductCategoryService ProductCategories { get; }
       public IClientTestimonialService ClientTestimonials { get; }
    }
}
