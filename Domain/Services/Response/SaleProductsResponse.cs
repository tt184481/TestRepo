using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Response
{
    public class SaleProductsResponse : BaseResponse<SaleProducts>
    {
        public SaleProductsResponse(SaleProducts saleProducts) : base(saleProducts) { }

        public SaleProductsResponse(string message) : base(message) { }

    }
}
