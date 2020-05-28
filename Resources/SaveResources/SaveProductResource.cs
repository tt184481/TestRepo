using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Resources.SaveResources
{
    public class SaveProductResource
    {
        [Required]
        public long ProductCode { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
