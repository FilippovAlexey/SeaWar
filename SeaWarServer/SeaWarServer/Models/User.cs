using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaWarServer.Models
{
    public class User
    {
        [JsonIgnore]
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
        public enum RoleEnum {None, Pirate, Trader}

    }
}