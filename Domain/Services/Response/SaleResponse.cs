using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Response
{
    public class SaleResponse : BaseResponse<Sale>
    {
        public SaleResponse(Sale sale) : base (sale) { }

        public SaleResponse(string message) : base(message) { }
    }
}
