using DataAccess.Helpers.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO.Response
{
    public class DataResponse<T>
    {
        public string Status { get; set; }
        public ResponseCode StatusCode { get; set; }
        public IEnumerable<T>? List { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
