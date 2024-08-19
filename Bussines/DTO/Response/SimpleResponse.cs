using DataAccess.Helpers.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines.DTO.Response
{
    public class SimpleResponse
    {
        public ResponseType Status { get; set; }
        public ResponseCode StatusCode { get; set; }
        public string Message { get; set; }
    }
   
}
