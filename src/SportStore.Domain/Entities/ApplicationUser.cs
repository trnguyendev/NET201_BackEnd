using Microsoft.AspNetCore.Identity;

namespace SportStore.Domain.Entities
{
    // Kế thừa IdentityUser để lấy hết các trường có sẵn của Microsoft
    public class ApplicationUser : IdentityUser
    {
        // Thêm các trường tùy biến của riêng bạn
        public string FullName { get; set; } = null!;

        // Tương lai có thể thêm: 
        // public string? AvatarUrl { get; set; }
        // public DateTime DateOfBirth { get; set; }
    }
}
