using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<PageResult<UserDto>> GetAllUserAsync(int pageNumber = 1, int pageSize = 20);
        Task<UserDto> GetUserByIdAsync(string id);
        Task UpdateUserAsync (string id, UpdateUserRequest request);
        Task ToggleUserStatusAsync (string id);
    }
}
