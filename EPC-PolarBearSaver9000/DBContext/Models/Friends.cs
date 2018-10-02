using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Friends
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }

        public AspNetUsers User1 { get; set; }
        public AspNetUsers User2 { get; set; }
    }
}
