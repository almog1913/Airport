namespace Models
{
    public class Terminal
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SecondsWaiting { get; set; }
        public Plane Plane { get; set; }
        public int NextTerminals { get; set; }
        public string Status { get; set; }
    }
}