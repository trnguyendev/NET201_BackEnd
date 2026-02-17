using Microsoft.AspNetCore.Http;

namespace SportStore.Application.Interfaces
{
    public interface IFileService
    {
        // Hàm này nhận file, thư mục lưu, và trả về đường dẫn file đã lưu
        Task<string> SaveFileAsync(IFormFile file, string subFolder, string[] allowedExtensions);

        // Hàm xóa file (quan trọng khi update sản phẩm mà muốn xóa ảnh cũ)
        void DeleteFile(string filePath);
    }
}
