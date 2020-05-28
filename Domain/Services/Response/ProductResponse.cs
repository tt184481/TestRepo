using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Response
{
    public class ProductResponse : BaseResponse<Product>
    {
        public ProductResponse(Product product) : base(product) { }

        public ProductResponse(string message) : base(message) { }
    }

}
