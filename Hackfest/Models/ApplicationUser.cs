namespace Hackfest.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
