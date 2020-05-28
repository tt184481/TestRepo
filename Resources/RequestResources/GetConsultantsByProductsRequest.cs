using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.RequestResources
{
    public class GetConsultantsByProductsRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinQuantity { get; set; }
        public long? ProductCode { get; set; }
    }
}
