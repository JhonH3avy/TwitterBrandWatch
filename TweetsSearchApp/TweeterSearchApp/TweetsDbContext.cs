using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TweeterSearchApp.Models;

namespace TweeterSearchApp
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
