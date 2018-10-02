using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPCPolarBearSaverAPI.Models
{
    public class FindAddress
    {
        public string Postcode { get; set; }
        public List<Addresses> ReturnedAddresses { get; set; }
        
    }
}
