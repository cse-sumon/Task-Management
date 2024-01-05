using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Helpers;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        // POST: {apibaseurl}/api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var loginResponseDto = await _authRepository.Login(request);

                    if (loginResponseDto is not null)
                    {
                        return Ok(loginResponseDto);
                    }
                }

                return BadRequest("Email or Password Incorrect");

            }
            catch (Exception ex)
            {
                throw;
            }
           
        }




        // POST: {apibaseurl}/api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Default Role Setup - (User)
                    if (request.Role == null || request.Role == "")
                    {
                        request.Role = RolesName.User;
                    }

                    var identityResult = await _authRepository.SignUp(request);

                    if (identityResult.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        if (identityResult.Errors.Any())
                        {
                            foreach (var error in identityResult.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }


        }


        // POST: {apibaseurl}/api/auth/register
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Roles = RolesName.Admin)]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                
               var users = await _authRepository.GetAllUsers();
                if(users is not null)
                     return Ok(users);

                return Ok(null);
            }
            catch (Exception)
            {

                throw;
            }


        }

    }
}
