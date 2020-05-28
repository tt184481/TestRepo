using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Product
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public long ProductCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int Price { get; set; }

        public ICollection<SaleProducts> SaleProducts { get; set; }
    }
}
