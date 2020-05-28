using System;
using System.Collections.Generic;
using System.Text;

namespace Resources.GetResources
{
    public class ConsultantSaleSumsResource
    {
        public int ConsultantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PrivateID { get; set; }
        public DateTime BirthDate { get; set; }
        public int OwnSaleSum { get; set; }
        public int AllSaleSum { get; set; }
    }
}
