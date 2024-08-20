using Bussines.DTO;
using Bussines.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services.Abstract
{
    public interface IUserService
    {
        Task<SimpleResponse> Register(RegisterDTO registerDto);
        Task<SimpleResponse> Login(LoginDTO loginDto);
        Task<DataResponse<UserDTO>> UserInfo(int userId);
    }
}
