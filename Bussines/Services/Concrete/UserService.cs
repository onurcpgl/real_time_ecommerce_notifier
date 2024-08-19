using AutoMapper;
using Bussines.DTO;
using Bussines.DTO.Response;
using Bussines.Services.Abstract;
using DataAccess.Helpers;
using DataAccess.Helpers.Enum;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserService(IUserService userService, IMapper mapper, IGenericRepository<User> genericRepository)
        {
            _userService = userService;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<SimpleResponse> Login(LoginDto loginDto)
        {
            var response = new SimpleResponse();
            try
            {
                var mapData = _mapper.Map<User>(loginDto);
                var result = await _genericRepository.Add(mapData);
                response.StatusCode = ResponseCode.OK;
                response.Status = ResponseType.Success;
                response.Message = "Login işlemi başarılı, yönlendiriliyorsunuz.";
                return response;
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                response.Status = ResponseType.Error;
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public async Task<SimpleResponse> Register(RegisterDto registerDto)
        {
            var response = new SimpleResponse();
            try
            {
                if(registerDto.Password == registerDto.ConfirmPassword)
                {
                    var mapData = _mapper.Map<User>(registerDto);

                    string hash = Sha256Helper.ComputeSha256Hash(mapData.Password);
                    mapData.Password = hash;
                    var result = await _genericRepository.Add(mapData);
                    response.StatusCode = ResponseCode.OK;
                    response.Status = ResponseType.Success;
                    response.Message = "Başarıyla kayıt oldunuz, yönlendiriliyorsunuz.";
                    return response;
                }
                else
                {
                    response.StatusCode = ResponseCode.BadRequest;
                    response.Status = ResponseType.Error;
                    response.Message = "Şifreler uyuşmuyor.";
                    return response;
                }
               
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                response.Status = ResponseType.Error;
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public Task<DataResponse<UserDTO>> UserInfo(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
