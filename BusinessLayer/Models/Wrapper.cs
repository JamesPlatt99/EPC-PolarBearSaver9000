using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPCPolarBearSaverAPI.Models
{
    public class Wrapper
    {
        public List<Rows> Result{ get; set; }
        public List<Rows> rows { get; set; }

        public Wrapper()
        {

        }
    }
}
