using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SportStore.Application.Interfaces;

namespace SportStore.Infrastructure.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string subFolder, string[] allowedExtensions)
        {
            var contentPath = _environment.WebRootPath;
            // Kết hợp đường dẫn: wwwroot + images + subFolder
            var path = Path.Combine(contentPath, "images", subFolder);

            // Tự động tạo folder nếu chưa tồn tại (Logic chuyên nghiệp nằm ở đây)
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(ext))
            {
                throw new ArgumentException($"File type {ext} is not allowed.");
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(path, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(stream);

            // Trả về đường dẫn tương đối để lưu DB: images/products/product-1/abc.jpg
            // Thay dấu \ bằng / để chuẩn web
            return Path.Combine("images", subFolder, fileName).Replace("\\", "/");
        }

        public void DeleteFile(string filePath)
        {
            // Logic xóa file cũ khi cần
            var fullPath = Path.Combine(_environment.WebRootPath, filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
