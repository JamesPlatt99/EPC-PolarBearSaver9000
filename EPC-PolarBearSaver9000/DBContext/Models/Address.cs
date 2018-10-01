using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Address
    {
        public int Id { get; set; }
        public string PostCode { get; set; }
        public string AddressLine1 { get; set; }
        public string UserId { get; set; }
    }
}
