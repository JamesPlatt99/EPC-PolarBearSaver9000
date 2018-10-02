using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPCPolarBearSaverAPI.Models
{
    public class Rows
    {
        public string address { get; set; }

        [DeserializeAs(Name = "inspection-date")]
        public DateTime inspectionDate { get; set; }
    }
}
