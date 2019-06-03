using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppSimulator.Models
{
    public class FlightData
    {
        public double? Lon { get; set; }
        public double? Lat { get; set; }
        public double? Throttle { get; set; }
        public double? Rudder { get; set; }
    }
}