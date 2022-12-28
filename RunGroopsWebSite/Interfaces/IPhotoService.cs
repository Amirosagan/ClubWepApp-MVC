namespace RunGroopsWebSite.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadPhotoAsync(IFormFile file);
        Task<string> DeletePhotoAsync(string url);
        Task<string> UpdatePhotoAsync(IFormFile file, string url);
    }
}
