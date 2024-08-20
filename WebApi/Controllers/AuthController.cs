using Bussines.DTO;
using Bussines.DTO.Response;
using Bussines.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/login")]
        public async Task<SimpleResponse> Login(LoginDTO loginDto)
        {
            var result = await _userService.Login(loginDto);
            return result;
         
        }

        [HttpPost("/register")]
        public async Task<SimpleResponse> Register(RegisterDTO registerDTO)
        {
            var result = await _userService.Register(registerDTO);
            return result;
        }
    }
}
