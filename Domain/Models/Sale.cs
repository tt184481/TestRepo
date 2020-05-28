using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Sale
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public int ConsultantID { get; set; }
        public Consultant Consultant { get; set; }

        public ICollection<SaleProducts> SaleProducts { get; set; }
    }
}
