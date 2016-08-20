using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class TurnData
    {
        public string ShipId { get; set; }
        public int TargetPosition { get; set; }
        public double Damage { get; set; }
    }
}