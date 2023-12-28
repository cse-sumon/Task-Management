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
        private readonly AuthDbContext _authContext;


        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<ApplicationUser> userManager, IConfiguration configuration, AuthDbContext authContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _authContext = authContext;


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

            var identityUser = await _userManager.FindByEmailAsync(loginDto.Email);

            if (identityUser is not null)
            {
                // Check Password
                var checkPasswordResult = await _userManager.CheckPasswordAsync(identityUser, loginDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    // Create a Token and Response
                    var jwtToken = CreateJwtToken(identityUser, roles.ToList());


                    response.FullName = identityUser.FullName;
                    response.Email = identityUser.Email;
                    response.Roles = roles.ToList();
                    response.Token = jwtToken;


                    return response;
                }
                
            }

            return null;

           
        }





        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // JWT Security Token Parameters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
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
