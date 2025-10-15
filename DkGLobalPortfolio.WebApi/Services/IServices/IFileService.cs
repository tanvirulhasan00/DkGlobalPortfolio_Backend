namespace DkGLobalPortfolio.WebApi.Services.IServices
{
    public interface IFileService
    {
        Task<string> FileUpload(IFormFile file, string folderName);
        void DeleteFile(string fileUrl);
    }
}
