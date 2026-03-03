namespace SportStore.Application.DTOs
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateUserRequest
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Role { get; set; }
    }
}
