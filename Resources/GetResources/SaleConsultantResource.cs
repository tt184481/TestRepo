using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.GetResources
{
    public class SaleConsultantResource
    {
        public int SaleID { get; set; }
        public DateTime SaleDate { get; set; }
        public int ConsultantID { get; set; }
        public string ConsultantFirstName { get; set; }
        public string ConsultantLastName { get; set; }
        public long ConsultantPrivateID { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductTotalMoney { get; set; }
    }
}
