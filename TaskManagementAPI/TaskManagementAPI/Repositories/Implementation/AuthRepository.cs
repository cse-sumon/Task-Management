using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models.Domain;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Repositories.Implementation
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;


        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;


        }



        public async Task<IdentityResult> SignUp(RegisterDto registerDto)
        {

            var user = new ApplicationUser
            {
                FullName = registerDto.FullName?.Trim(),
                UserName = registerDto.Email?.Trim(),
                Email = registerDto.Email?.Trim()
            };

            // Create User
            var identityResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (identityResult.Succeeded)
            {
                // Add Role to user (Reader)
                identityResult = await _userManager.AddToRoleAsync(user, registerDto.Role);

            }

            return identityResult;
        }




        public async Task<LoginResponseDto> Login(LoginRequestDto loginDto)
        {
            var response = new LoginResponseDto();

            var applicationUser = await _userManager.FindByEmailAsync(loginDto.Email);

            if (applicationUser is not null)
            {
                // Check Password
                var checkPasswordResult = await _userManager.CheckPasswordAsync(applicationUser, loginDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(applicationUser);

                    // Create a Token and Response
                    var jwtToken = CreateJwtToken(applicationUser, roles.ToList());


                    response.FullName = applicationUser.FullName;
                    response.Email = applicationUser.Email;
                    response.Roles = roles.ToList();
                    response.Token = jwtToken;


                    return response;
                }
                
            }

            return null;

           
        }





        public string CreateJwtToken(ApplicationUser user, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // JWT Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            return users.Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                UserName = x.UserName,

            }).ToList();

        }



    }
}
