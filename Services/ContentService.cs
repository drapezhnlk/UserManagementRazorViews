using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using UserManagementRazorViews.Interfaces;

namespace UserManagementRazorViews.Services
{
    public class ContentService : IContentService
    {   
        private readonly int _resizeHeight;
        private readonly int _resizeWidth;
        private readonly string _originalImagesFolderAbsolutePath;

        public ContentService(IConfiguration config, IWebHostEnvironment hostEnvironment)
        {
            _originalImagesFolderAbsolutePath = Path.Combine(
                hostEnvironment.WebRootPath,
                config.GetSection("ImagesFolderNames")["Original"]);

            _resizeHeight = int.Parse(config.GetSection("ImageSizes")["UserIconHeight"]);
            _resizeWidth = int.Parse(config.GetSection("ImageSizes")["UserIconWidth"]);

            if (!Directory.Exists(_originalImagesFolderAbsolutePath))
                Directory.CreateDirectory(_originalImagesFolderAbsolutePath);
        }

        public async Task<byte[]> ResizeOriginalImageAsync(string imageName)
        {
            if (string.IsNullOrEmpty(imageName)) return null;

            var originalFilePath = Path.Combine(_originalImagesFolderAbsolutePath, imageName);
            
            using var image = Image.Load(originalFilePath);
            image.Mutate(i => i.Resize(_resizeWidth, _resizeHeight));
            
            await using var resizedStream = new MemoryStream();
            await Task.Run(() => image.SaveAsJpeg(resizedStream));

            return resizedStream.ToArray();
        }

        public async Task<string> SaveUserPhotoAsync(IFormFile userPhotoFile)
        {
            if (userPhotoFile == null) return string.Empty;

            var photoName = $"{Guid.NewGuid()}_{userPhotoFile.FileName}";
            var filePath = Path.Combine(_originalImagesFolderAbsolutePath, photoName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await userPhotoFile.CopyToAsync(stream);

            return photoName;
        }
    }
}