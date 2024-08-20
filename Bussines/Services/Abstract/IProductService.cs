using Bussines.DTO.Response;
using Bussines.DTO;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.Services.Abstract
{
    public interface IProductService 
    {
        Task<SimpleResponse> AddProduct(ProductDTO productDto);
        Task<DataResponse<ProductDTO>> ListProduct();
        Task<SimpleResponse> UpdatedStock(int id, int quantityChange);
        Task<SimpleResponse> UpdatedProduct(ProductDTO productDto,int id);

    }
}
