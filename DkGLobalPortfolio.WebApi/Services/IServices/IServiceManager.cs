namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IServiceManager
    {
        Task<int> Save();
       public IPostService Posts { get; }
       public IBlogPostTagService BlogPostTags { get; }
    }
}
