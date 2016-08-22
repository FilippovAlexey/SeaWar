using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    [NotMapped]
    public class ShipInBattle : Ship
    {
        public int TargetPosition { get; set; }
        public ShipAction Action { get; set; }
        public BattleOwner Owner { get; set; }
        public enum ShipAction { Nothing, Attack }
        public enum BattleOwner { Host, Player}

        public ShipInBattle(Ship ship)
        {
            this.Damage = ship.Damage;
            this.Health = ship.Health;
            this.Id = ship.Id;
            this.Name = ship.Name;
            this.Speed = ship.Speed;
        }

        public void Restore()
        {
            this.Action = ShipAction.Nothing;
            this.TargetPosition = 0;
        }
       
    }
}