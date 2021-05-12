using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Flights
    {
        public long Id { get; set; }
        public string OriginCountry { get; set; }
        public string DestCountry { get; set; }
        public long Remaining { get; set; }
    }
}