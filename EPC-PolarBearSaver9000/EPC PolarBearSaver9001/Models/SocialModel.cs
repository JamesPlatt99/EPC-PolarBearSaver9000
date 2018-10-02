using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBContext.Models;

namespace EPC_PolarBearSaver9001.Models
{
    public class SocialModel
    {
        public List<AspNetUsers> Friends { get; set; }
        public List<AspNetUsers> SearchResults { get; set; }

        public string SearchQuery { get; set; }
        public string UserID { get; set; }
    }
}
