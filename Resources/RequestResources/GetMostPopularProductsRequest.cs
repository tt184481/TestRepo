using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.RequestResources
{
    public class GetMostPopularProductsRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
