using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Response
{
    public class ConsultantResponse : BaseResponse<Consultant>
    {
        public ConsultantResponse(Consultant consultant) : base(consultant) { }

        public ConsultantResponse(string message) : base(message) { }
    }
}
