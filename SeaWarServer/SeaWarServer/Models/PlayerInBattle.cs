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
            this.ShipList.AddRange(dbContext.Users.First(u => u.Id == this.Id).Ships.Take(5) as List<ShipInBattle>);
        }

        public event EventHandler ReadyChanged; 
    }
}