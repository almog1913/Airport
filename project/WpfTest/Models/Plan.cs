namespace Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string Flight { get; set; }
        public PlanStatus Status { get; set; }
        public int Color { get; set; }
    }
}