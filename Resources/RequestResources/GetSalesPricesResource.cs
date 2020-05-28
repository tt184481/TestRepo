using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.RequestResources
{
    public class GetSalesPricesResource
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
}
