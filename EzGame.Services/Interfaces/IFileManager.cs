using System.Threading.Tasks;
using EzGame.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EzGame.Services.Interfaces
{
    public interface IFileManager 
    {
        Task<string> UploadImage(IFormFile image, FileManagerType.FileType typeI);
        bool DeleteImage(string imageName, FileManagerType.FileType typeI);
        Task<string> CkUpload(IFormFile image);
    }
}