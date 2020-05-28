using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SaleProducts
    {
        public int ID { get; set; }
        public int ProductQuantity { get; set; }


        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int SaleID { get; set; }
        public Sale Sale { get; set; }
    }
}
