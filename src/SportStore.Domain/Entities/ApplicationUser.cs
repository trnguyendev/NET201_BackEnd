using Microsoft.AspNetCore.Identity;

namespace SportStore.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
    }
}
