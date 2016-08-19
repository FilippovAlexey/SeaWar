using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class ShipInBattle : Ship
    {
        public string TargetId { get; set; }
        public ShipAction Action { get; set; }
        public enum ShipAction { Nothing, Attack }
    }
}