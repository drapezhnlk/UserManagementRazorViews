using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UserManagementRazorViews.Interfaces
{
    public interface IContentService
    {
        Task<byte[]> ResizeOriginalImageAsync(string imageName);
        Task<string> SaveUserPhotoAsync(IFormFile userPhotoFile);
    }
}