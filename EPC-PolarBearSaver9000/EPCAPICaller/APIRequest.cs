using EPCPolarBearSaverAPI.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace EPCAPICaller
{
    public class APIRequest
    {
        private const int SIZE = 99;
        public static IEnumerable<string> GetAddresses(string postcode)
        {
            IEnumerable<string> addresses = GetData(postcode).Select(n=>n.Address);            
            return addresses;
        }

        public static int GetScore(string postcode, string address)
        {
            IEnumerable<Rows> data = GetData(postcode);
            Rows rowAtAddress = data.Where(n => n.Address == address).SingleOrDefault();
            int score = rowAtAddress?.EnvironmentImpactCurrent ?? 0;
            return score;
        }

        private static IEnumerable<Rows> GetData(string postcode)
        {
            var client = new RestClient("https://dceas-user-site-staging.cloudapps.digital");
            var request = new RestRequest($"api/epc?postcode={postcode}&size={SIZE}", Method.GET);
            IRestResponse response = client.Execute(request);
            var results = JsonConvert.DeserializeObject<Wrapper>(response.Content);
            var data = new List<Rows>();
            if (results != null)
            {
                var rows = results.rows.OrderBy(n => n.InspectionDate).GroupBy(n => n.Address).Select(n => n.First());
                data.AddRange(rows);
            }
            return data;
        }

    }
}
