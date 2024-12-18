using SITS_API.Application.DTOs;
using SITS_API.Domain.Entities;

namespace SITS_API.Application.Contracts
{
    public interface IUser
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
        Task<IEnumerable<ApplicationUser>> GetUserDetails();
        Task<ApplicationUser> GetUserDetailsById(int id);
        Task<IEnumerable<ApplicationUser>> UpdateUser(ApplicationUser user);
    }
}
