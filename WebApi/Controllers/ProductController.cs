using Bussines.DTO.Response;
using Bussines.DTO;
using Bussines.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("/add-product")]
        public async Task<SimpleResponse> AddProduct(ProductDTO productDto)
        {
            var result = await _productService.AddProduct(productDto);
            return result;
        }

        [HttpGet("/list-product")]
        public async Task<DataResponse<ProductDTO>> ListProduct()
        {
            var result = await _productService.ListProduct();
            return result;
        }

        [HttpPut("/updated-product-stock")]
        public async Task<SimpleResponse> UpdatedStock(int productId, int quantityChange)
        {
            var result = await _productService.UpdatedStock(productId, quantityChange);
            return result;
        }

        [HttpPut("/updated-product")]
        public async Task<SimpleResponse> UpdatedProduct(ProductDTO productDto,int id)
        {
            var result = await _productService.UpdatedProduct(productDto, id);
            return result;
        }
    }
}
