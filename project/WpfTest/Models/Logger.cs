using System;

namespace Models
{
    public class Logger
    {
        public int Id { get; set; }
        public int Terminal { get; set; }
        public string Plane { get; set; }
        public string PlaneStatus { get; set; }
        public int PlaneColor { get; set; }
        public DateTime Time { get; set; }
    }
}