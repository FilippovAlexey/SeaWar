using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaWarServer.Models
{
    public class PlayerInBattle
    {
        private bool ready = false;
        public string Id { get; set; }
        public bool Ready
        {
            get
            {
                return ready;
            }
            set
            {
                ready = value;
                if (ready)
                {
                    ReadyChanged(this, null);
                }
            }
        }
        public List<ShipInBattle> ShipList { get; set; }

        //TODO:do something with it!
        internal void AutoAddShips()
        {
            DataBaseContext dbContext = new DataBaseContext();
            List<ShipInBattle> sl = dbContext.Users.First(u => u.Id == this.Id).Ships.Take(5).Select(a=>new ShipInBattle(a)).ToList();
            this.ShipList = new List<ShipInBattle>();
            this.ShipList.AddRange(sl);
        }

        public event EventHandler ReadyChanged; 
    }
}