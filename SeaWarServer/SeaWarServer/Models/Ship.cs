namespace SeaWarServer.Models
{
    public class Ship
    {
        private double health;
        public string Id { get; set; }
        public string Name { get; set; }
        public ShipType Type { get; set; }
        public double HoldSize { get; set; }
        public double Health
        {
            get
            {
                return health;
            }
            set
            {
                if(value<0)
                {
                    health = 0;
                }
                else
                {
                    health = value;
                }
            }
        }
        public double Speed { get; set; }
        public double Damage { get; set; }

        public enum ShipType { Lugger, Sloop, HeavySloop,  Barque, Schooner, Caravel, Barkentine, Brigantine }
    }
}