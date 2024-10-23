using LinkDev.Talabat.Core.Abstraction;
using Microsoft.AspNetCore.Http;

namespace LinkDev.Talabat.Core.Application.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _allowedMaxSize = 2_097_152;


        #region Upload File

        public async Task<string?> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file.Length == _allowedMaxSize)
                return null;

            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
                return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            if (Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return Path.Combine($"images/{folderName}/", fileName);
        }

        #endregion

        #region Delete Attachment

        public bool DeleteAttachment(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        #endregion
    }
}
