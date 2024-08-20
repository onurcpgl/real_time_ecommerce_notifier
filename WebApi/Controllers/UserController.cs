using Bussines.DTO.Response;
using Bussines.DTO;
using Bussines.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/user-info")]
        public async Task<DataResponse<UserDTO>> Register(int id)
        {
            var result = await _userService.UserInfo(id);
            return result;
        }
    }
}
