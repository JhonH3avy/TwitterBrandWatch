using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TweetManualCategorizer.Models
{
    public partial class TweetDataWarehouseContext : DbContext
    {
        public TweetDataWarehouseContext()
        {
        }

        public TweetDataWarehouseContext(DbContextOptions<TweetDataWarehouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tweets> Tweets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-AN4MSQE\\SQLEXPRESS;Database=TweetDataWarehouse;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweets>(entity =>
            {
                entity.HasKey(e => e.TweetId);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
