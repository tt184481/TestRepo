using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.GetResources
{
    public class SaleProductPriceResource
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int ConsultantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonalID { get; set; }
        public int DiffProductQuantity { get; set; }
    }
}
