using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Consultant
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public long PersonalID { get; set; }
        [Required]
        public bool Gender { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public int RecommendatoryID { get; set; }
        public virtual Consultant Recommendatory { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
