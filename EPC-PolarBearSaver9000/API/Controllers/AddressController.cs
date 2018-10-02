using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EPCAPICaller;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // GET api/address/{postcode}
        [HttpGet("{postcode}")]
        [EnableCors("MyPolicy")]
        public ActionResult<List<string>> Get(string postcode)
        {
            var addresses = APIRequest.GetAddresses(postcode);
            return addresses.ToList();
        }
    }
}
