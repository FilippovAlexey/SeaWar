using SeaWarServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaWarServer
{
    public static class Statics
    {
        public static List<Ship> ShipList = new List<Ship>();
        public static List<BattleSession> BattleSessionList = new List<BattleSession>();
        static Statics()
        {
            ShipList.Add(new Ship() { Name = "Log", Health = 50, Damage = 12, Speed = 3 });
            ShipList.Add(new Ship() { Name = "Washtub", Health = 42, Damage = 15, Speed = 4 });

        }
    }
}