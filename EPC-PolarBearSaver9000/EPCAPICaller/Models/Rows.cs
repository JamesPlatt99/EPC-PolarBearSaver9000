using Newtonsoft.Json;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPCPolarBearSaverAPI.Models
{
    public class Rows
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("inspection-date")]
        public DateTime InspectionDate { get; set; }

        [JsonProperty("environment-impact-current")]
        public int EnvironmentImpactCurrent { get; set; }
    }
}
