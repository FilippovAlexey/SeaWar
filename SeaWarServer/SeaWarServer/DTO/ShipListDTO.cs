using System.Collections.Generic;

namespace SeaWarServer.DTO
{
    public class ShipListDTO
    {
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public List<string> ShipList { get; set; }
    }
}