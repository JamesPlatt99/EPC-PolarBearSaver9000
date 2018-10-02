using EPCPolarBearSaver9001.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EPCPolarBearSaverAPI.Models;

namespace EPCPolarBearSaver9001.Models
{
    public class AddressFinder
    {
        [DisplayName("Postcode")]
        public string Postcode { get; set; }

        [DisplayName("First Line")]
        public string Address1 { get; set; }

        public List<Rows> ListOfAddresses { get; set; }

        public List<Rows> rows { get; set; }

        public IEnumerable<string> Bubbles { get; set; }
    }
}
