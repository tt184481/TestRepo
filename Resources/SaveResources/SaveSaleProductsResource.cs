using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Resources.SaveResources
{
    public class SaveSaleProductsResource
    {
        [Required]
        public int ProductQuantity { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int SaleID { get; set; }
    }
}
