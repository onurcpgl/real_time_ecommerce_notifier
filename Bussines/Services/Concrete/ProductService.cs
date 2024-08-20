using AutoMapper;
using Bussines.DTO;
using Bussines.DTO.Response;
using Bussines.Services.Abstract;
using DataAccess.Helpers.Enum;
using DataAccess.Repositories.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _genericRepository;

        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IGenericRepository<Product> genericRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<SimpleResponse> AddProduct(ProductDTO productDto)
        {
            var response = new SimpleResponse();

            try
            {
                var mapProduct = _mapper.Map<Product>(productDto);
                var result = await _genericRepository.Add(mapProduct);

                if(result)
                {
                    response.Status = ResponseType.Success.ToString();
                    response.StatusCode = ResponseCode.OK;
                    response.Message = "Ürün başarıyla eklendi.";
                    return response;
                }
                else
                {
                    string responseString = ResponseType.Error.ToString();
                    response.Status = responseString;
                    response.StatusCode = ResponseCode.BadGateway;
                    response.Message = "Ürün eklenirken bir sorun meydana geldi.";
                    return response;
                }
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                response.Status = ResponseType.Error.ToString();
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public async Task<DataResponse<ProductDTO>> ListProduct()
        {
            var response = new DataResponse<ProductDTO>();

            try
            {
                var result = _genericRepository.GetAll();

                if(result != null)
                {
                    response.Status = ResponseType.Success.ToString();
                    response.StatusCode = ResponseCode.OK;
                    var mapResult = _mapper.Map<List<ProductDTO>>(result);
                    response.List = mapResult;
                    return response;
                }
                else
                {
                    response.Status = ResponseType.Error.ToString(); ;
                    response.StatusCode = ResponseCode.BadGateway;           
                    return response;
                }
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                response.Status = ResponseType.Error.ToString();
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }

        public async Task<SimpleResponse> UpdatedProduct(ProductDTO productDto, int id)
        {
            var response = new SimpleResponse();

            try
            {
                //Map product
                var newMapData = _mapper.Map<Product>(productDto);
                // İlgili product
                var product = await _genericRepository.GetById(id);

                if (!string.IsNullOrEmpty(newMapData.Name) && newMapData.Name != product.Name)
                {
                    product.Name = newMapData.Name;
                }

                if (!string.IsNullOrEmpty(newMapData.Description) && newMapData.Description != product.Description)
                {
                    product.Description = newMapData.Description;
                }

                if (newMapData.Price != 0 && newMapData.Price != product.Price)
                {
                    product.Price = newMapData.Price;
                }

                if (newMapData.Stock != product.Stock)
                {
                    product.Stock = newMapData.Stock;
                }

                product.Id = id;

                var result = _genericRepository.Update(product);

                if(result)
                {
                    string responseString = ResponseType.Success.ToString();
                    response.Status = responseString;
                    response.StatusCode = ResponseCode.OK;
                    response.Message = "Ürün güncellendi.";
                    return response;
                }
                else
                {
                  
                    response.Status = ResponseType.Error.ToString();
                    response.StatusCode = ResponseCode.BadRequest;
                    response.Message = "Ürün güncellenirken hata meydana geldi.";
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

        public async Task<SimpleResponse> UpdatedStock(int id, int quantityChange)
        {
            var response = new SimpleResponse();

            try
            {
                var product = await _genericRepository.GetById(id);
                if(product == null)
                {
                    response.StatusCode = ResponseCode.NotFound;
                    response.Status = ResponseType.Error.ToString();
                    response.Message = "Ürün bulunamadı.";
                    return response;
                }

                product.Stock += quantityChange;

                if (product.Stock < 0)
                {
                    response.StatusCode = ResponseCode.BadRequest;
                    response.Status = ResponseType.Error.ToString();
                    response.Message = "Stok miktarı negatif olamaz.";
                    return response;
                }

                _genericRepository.Update(product);
                response.StatusCode = ResponseCode.OK;
                response.Status = ResponseType.Success.ToString();
                response.Message = "Ürün başarıyla güncellendi.";
                return response;
                
            }
            catch (Exception ex)
            {

                response.StatusCode = ResponseCode.BadRequest;
                response.Status = ResponseType.Error.ToString();
                response.Message = $"Bir hata meydana geldi! : {ex.Message}";

                return response;
            }
        }
    }
}
