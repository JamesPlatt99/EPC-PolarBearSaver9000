using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPCPolarBearSaverAPI.Models
{
    public class Rows
    {
        [DeserializeAs(Name = "address")]
        public string Address { get; set; }

        [DeserializeAs(Name = "inspection-date")]
        public DateTime InspectionDate { get; set; }

        [DeserializeAs(Name = "environment-impact-current")]
        public int EnvironmentalImpactCurrent { get; set; }
    }
}
