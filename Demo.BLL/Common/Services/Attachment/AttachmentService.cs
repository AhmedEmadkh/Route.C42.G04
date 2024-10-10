using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.Attachment
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new() { ".png", ".jpg", ".jpeg" };
        private const int _allowedMaxSize = 2_097_152;
        public async Task<string?> UploadAsync(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName); // Ahmed.png => .png (FileName not Name)

            if(!_allowedExtensions.Contains(extension))
                return null;

            if(file.Length > _allowedMaxSize) 
                return null;


            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if(!Directory.Exists(folderPath)) // If Not Found Create New One
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{extension}"; // Must Be Unique

            var filePath = Path.Combine(folderPath, fileName); // File Place

            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream); // Opens the Stream and Create the File

            return fileName; // Return File Name Bec. it is UNIQUE
        }
        public bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
