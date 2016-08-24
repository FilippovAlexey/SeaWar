using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SeaWarServer.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public virtual List<Ship> Ships { get; set; }
        public RoleEnum Role { get; set; }

        public User() { }

        public User(string Name, string Password)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = Name;
            this.Password = Password;
        }

        public string SetRole(RoleEnum role)
        {
            Ship tempShip;
            switch (role)
            {
                case Models.User.RoleEnum.Pirate:
                    this.Role = Models.User.RoleEnum.Pirate;
                    tempShip = Statics.ShipList.First(s => s.Type == Ship.ShipType.Sloop);
                    tempShip.Id = Guid.NewGuid().ToString();
                    this.Ships.Add(tempShip);
                    return Messages.Success;
                case Models.User.RoleEnum.Smuggler:
                    this.Role = Models.User.RoleEnum.Smuggler;
                    tempShip = Statics.ShipList.First(s => s.Type == Ship.ShipType.Lugger);
                    tempShip.Id = Guid.NewGuid().ToString();
                    this.Ships.Add(tempShip);
                    return Messages.Success;
                case Models.User.RoleEnum.HeadHunter:
                    this.Role = Models.User.RoleEnum.HeadHunter;
                    tempShip = Statics.ShipList.First(s => s.Type == Ship.ShipType.HeavySloop);
                    tempShip.Id = Guid.NewGuid().ToString();
                    this.Ships.Add(tempShip);
                    return Messages.Success;
                default:
                    return Messages.RoleMissing;
            }
        }
        public enum RoleEnum {None, Pirate, Smuggler, HeadHunter }

    }
}