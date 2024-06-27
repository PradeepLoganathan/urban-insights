namespace UrbanInsights.Api.Models
{
    
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}