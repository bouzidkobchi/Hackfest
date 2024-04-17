namespace Hackfest.Models
{
    public class News
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Pictures { get; set; }
        public DateTime CreatedAt { get; private set; }
        public News()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
