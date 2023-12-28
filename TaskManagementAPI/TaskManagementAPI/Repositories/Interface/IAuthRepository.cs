using Microsoft.AspNetCore.Identity;
using TaskManagementAPI.Models.DTO;

namespace TaskManagementAPI.Repositories.Interface
{
    public interface IAuthRepository
    {

        Task<IdentityResult> SignUp(RegisterDto registerDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginDto);


        Task<IEnumerable<UserDto>> GetAllUsers();

    }
}
