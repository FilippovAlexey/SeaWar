using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class ShipInBattle : Ship,IComparer, IComparable
    {
        public int TargetPosition { get; set; }
        public ShipAction Action { get; set; }
        public BattleOwner Owner { get; set; }
        public enum ShipAction { Nothing, Attack }
        public enum BattleOwner { Host, Player}

        public void Restore()
        {
            this.Action = ShipAction.Nothing;
            this.TargetPosition = 0;
        }
        public int Compare(object x, object y)
        {
            var X = (ShipInBattle)x;
            var Y = (ShipInBattle)y;
            if (X.Speed > Y.Speed)
                return 1;
            else if (X.Speed < Y.Speed)
                return -1;
            else return 0;
        }

        public int CompareTo(object obj)
        {
            var ship = (ShipInBattle)obj;
            return Compare(this, ship);
        }
    }
}