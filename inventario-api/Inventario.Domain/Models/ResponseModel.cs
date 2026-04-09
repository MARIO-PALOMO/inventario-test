using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.Models
{
    public class ResponseModel
    {
        public class BaseResponse
        {
            public string Message { get; set; }
            public bool Status { get; set; }
            public int? Total { get; set; } = 0;
        }

        public class IResponseModel<D> : BaseResponse
        {
            public D? Data { get; set; }
        }
    }
}
