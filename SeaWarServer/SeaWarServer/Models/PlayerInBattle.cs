using System;
using System.Collections.Generic;

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
                ReadyChanged(this, null);
            }
        }
        public List<Ship> ShipList { get; set; }

        public event EventHandler ReadyChanged;
    }
}