using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Twitter.Models
{
    public class TweetsDbContext : DbContext
    {
        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        }
    }
}
