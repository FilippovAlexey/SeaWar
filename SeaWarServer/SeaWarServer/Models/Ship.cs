using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class Ship
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
    }
}