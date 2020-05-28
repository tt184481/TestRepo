using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.GetResources
{
    public class ConsultantPopularProductResource
    {
        public int ConsultantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PrivateID { get; set; }
        public DateTime BirthDate { get; set; }
        public long PopularProductCode { get; set; }
        public string PopularProductName { get; set; }
        public int ProductQuantity { get; set; }
        public long ProfitableProductCode { get; set; }
        public string ProfitableProductName { get; set; }
        public int Profit { get; set; }
    }
}
