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
    public class ScoreController : ControllerBase
    {
        // GET api/score/{postcode}/{address}
        [HttpGet("{postcode}/{address}")]
        [EnableCors("MyPolicy")]
        public ActionResult<int> Get(string postcode, string address)
        {
            int score = APIRequest.GetScore(postcode, address);
            return score;
        }
    }
}
