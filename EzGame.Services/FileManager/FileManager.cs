using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using EzGame.Domain.Enums;
using EzGame.Services.Interfaces;

namespace EzGame.Services.FileManager
{
    public class FileManager : IFileManager
    {
        private readonly string _webRootPath;

        public FileManager(string webRootPath)
        {
            _webRootPath = webRootPath;
        }


        public async Task<string> UploadImage(IFormFile image, FileManagerType.FileType typeId)
        {
            var type = FileManagerType.ParseType(typeId);
            var uploadPath = Path.Combine(_webRootPath + "/" + type);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (image.ContentType == "image/jpeg" || image.ContentType == "image/svg+xml" || image.ContentType == "image/png")
            {
                var filePath = Path.Combine(uploadPath, image.FileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(fileStream).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("please enter a image to upload :D");
            }

            return image.FileName;
        }

        public bool DeleteImage(string imageName, FileManagerType.FileType typeId)
        {
            var type = FileManagerType.ParseType(typeId);
            var image = Path.Combine(_webRootPath + "/" + type, imageName);
            if (!File.Exists(image)) return false;
            File.Delete(image);
            return true;

        }

        public async Task<string> CkUpload(IFormFile image)
        {
            var uploadPath = Path.Combine(_webRootPath + "/ckeditorImages");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            if (image.ContentType == "image/jpeg" || image.ContentType == "image/svg+xml" || image.ContentType == "image/png")
            {
                var filePath = Path.Combine(uploadPath, image.FileName);
                await using var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(fileStream).ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentException("please enter a image to upload :D");
            }

            return image.FileName;
        }
    }
}