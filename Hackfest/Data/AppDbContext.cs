using Hackfest.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackfest.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Activity> Activities {  get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Article> Articles { get; set; }

        public AppDbContext()
        {
            //Activities = [];
            //News = [];
            //Users = [];
            //Articles = [];
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HackFest;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
