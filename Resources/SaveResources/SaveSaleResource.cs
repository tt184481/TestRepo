using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Resources.SaveResources
{
    public class SaveSaleResource
    {
        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public int ConsultantID { get; set; }
    }
}
