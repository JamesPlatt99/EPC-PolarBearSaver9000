using EPCPolarBearSaverAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace EPCAPICaller
{
    public class APIRequest
    {
        public static IEnumerable<string> GetAddresses(string postcode)
        {
            string size = "99";

            var client = new RestClient("https://dceas-user-site-staging.cloudapps.digital");
            var request = new RestRequest($"api/epc?postcode={postcode}&size={size}", Method.GET);
            IRestResponse response2 = client.Execute(request);

            Wrapper wrapper = JsonConvert.DeserializeObject<Wrapper>(response2.Content);
            List<string> rows = new List<string>();
            var response = wrapper?.rows.OrderBy(n => n.inspectionDate).GroupBy(n => n.address).Select(n => n.First().address);
            if(response != null)
            {
                rows.AddRange(response);
            }
            return rows;
        }

        public static IEnumerable<string> GetResults(string postcode)
        {
            //string postcode = "NN72PS";
            string size = "99";

            var client = new RestClient("https://dceas-user-site-staging.cloudapps.digital");
            var request = new RestRequest($"api/epc?postcode={postcode}&size={size}", Method.GET);
            IRestResponse response2 = client.Execute(request);

            Wrapper wrapper = JsonConvert.DeserializeObject<Wrapper>(response2.Content);

            return wrapper.rows.Where(n=>n.address.Equals(postcode)).Select(n=>n.address).OrderBy(n=>n);
        }

    }
}
