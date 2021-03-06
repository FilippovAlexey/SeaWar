﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SeaWarServer.Models
{
    public class PlayerInBattle
    {
        private bool ready;
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
                    ReadyChanged(this, EventArgs.Empty);
                }
            }
        }
        public List<ShipInBattle> ShipList { get; set; }

        public PlayerInBattle(string id)
        {
            this.Ready = false;
            this.Id = id;
            this.ShipList = new List<ShipInBattle>();
        }

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