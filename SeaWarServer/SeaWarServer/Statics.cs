using SeaWarServer.Models;
using System.Collections.Generic;

namespace SeaWarServer
{
    public static class Statics
    {
        public static List<Ship> ShipList = new List<Ship>();
        public static List<BattleSession> BattleSessionList = new List<BattleSession>();
        static Statics()
        {
            ShipList.Add(new Ship() { Name = "Lugger", Type=Ship.ShipType.Lugger, Health = 60, Damage = 12, Speed = 3 ,HoldSize=80 });
            ShipList.Add(new Ship() { Name = "Sloop", Type = Ship.ShipType.Sloop, Health = 40, Damage = 15, Speed = 4 , HoldSize = 50});
            ShipList.Add(new Ship() { Name = "HeavySloop", Type = Ship.ShipType.HeavySloop, Health = 50, Damage = 15, Speed =3, HoldSize = 30 });

        }
    }
}