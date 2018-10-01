using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Address
    {
        public Address()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Id { get; set; }
        public string PostCode { get; set; }
        public string AddressLine1 { get; set; }

        public ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
