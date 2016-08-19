using SeaWarServer.Models;
using System.Collections.Generic;

namespace SeaWarServer.DTO
{
    public class TurnDataDTO
    {
        public string PlayerId { get; set; }
        public string SessionId { get; set; }
        public List<ShipInBattle> MyProperty { get; set; }
    }
}