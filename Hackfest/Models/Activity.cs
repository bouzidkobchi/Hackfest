namespace Hackfest.Models
{
    public class Activity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Pictures { get; set; }
        public DateTime StartAt { get; private set; }
        public DateTime EndAt { get; private set; }
        public Activity()
        {
            StartAt = DateTime.Now;
            EndAt = DateTime.Now;
        }
    }
}
