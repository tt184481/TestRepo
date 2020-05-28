using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.RequestResources
{
    public class ConsultantResource
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PersonalID { get; set; }
        public bool Gender { get; set; } 
        public DateTime BirthDate { get; set; }
        public int RecommendatoryID { get; set; }
    }
}
