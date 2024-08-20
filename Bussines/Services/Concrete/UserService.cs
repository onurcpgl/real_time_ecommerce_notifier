using AutoMapper;
using Bussines.DTO;
using Bussines.DTO.Response;
using Bussines.Services.Abstract;
using DataAccess.Helpers;
using DataAccess.Helpers.Enum;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
      
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, IGenericRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<SimpleResponse> Login(LoginDTO loginDto)
        {
            var response = new SimpleResponse();
            try
            {
                //User map
                var mapData = _mapper.Map<User>(loginDto);
                string hash = Sha256Helper.ComputeSha256Hash(mapData.Password);

                //User find
                var result = await _genericRepository.GetWhereWithInclude(x => x.Email == mapData.Email && x.Password == hash, true).FirstOrDefaultAsync();
                if(result?.Password == hash)
                {
                    response.StatusCode = ResponseCode.OK;
                    string responseString = ResponseType.Success.ToString();
                    response.Status = responseString;
                    response.Message = "Login işlemi başarılı, yönlendiriliyorsunuz.";
                    return response;
                }
                else
                {
                    response.StatusCode = ResponseCode.BadGateway;
                    string responseString = ResponseType.Error.ToString();
                    response.Status = responseString;
                    response.Message = "Şifre yada email adresi yanlış.";
                    return response;
                }
               
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                string responseString = ResponseType.Error.ToString();
                response.Status = responseString;
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public async Task<SimpleResponse> Register(RegisterDTO registerDto)
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
                    string responseString = ResponseType.Success.ToString();
                    response.Status = responseString;
                    response.Message = "Başarıyla kayıt oldunuz, yönlendiriliyorsunuz.";
                    return response;
                }
                else
                {
                    response.StatusCode = ResponseCode.BadRequest;
                    string responseString = ResponseType.Error.ToString();
                    response.Status = responseString;
                    response.Message = "Şifreler uyuşmuyor.";
                    return response;
                }
               
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                string responseString = ResponseType.Error.ToString();
                response.Status = responseString;
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public async Task<DataResponse<UserDTO>> UserInfo(int userId)
        {
            var response = new DataResponse<UserDTO>();

            try
            {
                var user = await _genericRepository.GetById(userId);

                if (user != null)
                {
                    var mapUser = _mapper.Map<UserDTO>(user);

                    response.Data = mapUser;
                    string responseString = ResponseType.Success.ToString();
                    response.Status = responseString;
                    response.StatusCode = ResponseCode.OK;

                    return response;

                }else
                {
                    string responseString = ResponseType.Success.ToString();
                    response.Status = responseString;
                    response.StatusCode = ResponseCode.NotFound;
                    response.Message = "Kullanıcı bulunamadı.";
                    return response;

                }

                
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                string responseString = ResponseType.Error.ToString();
                response.Status = responseString;
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }
    }
}
