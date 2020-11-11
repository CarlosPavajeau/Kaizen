namespace Kaizen.Domain.Entities
{
    public abstract class Statistics
    {
        public int AppliedActivities { get; set; }
        public int ClientsVisited { get; set; }
        public int ClientsRegistered { get; set; }
        public decimal Profits { get; set; }
    }
}
