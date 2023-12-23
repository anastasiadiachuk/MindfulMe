using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ProgramDbContext : DbContext
    {
        public ProgramDbContext(DbContextOptions<ProgramDbContext> contextOptions) : base(contextOptions) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Email = "user1@example.com", Password = "password", UserName = "user1", UserType = UserType.User },
                new User { UserId = 2, Email = "user2@example.com", Password = "password", UserName = "user2", UserType = UserType.Admin }
            );

            // Seed Articles
            modelBuilder.Entity<Article>().HasData(
                new Article { ArticleId = 1, Title = "Article 1", Description = "Description 1", Content = "Content 1", AuthorUsername = "user2" },
                new Article { ArticleId = 2, Title = "Article 2", Description = "Description 2", Content = "Content 2", AuthorUsername = "user2" }
            );

            // Seed Offers
            modelBuilder.Entity<Offer>().HasData(
                new Offer { OfferId = 1, Description = "Offer 1", Content = "Offer Content 1" },
                new Offer { OfferId = 2, Description = "Offer 2", Content = "Offer Content 2" }
            );
        }
    }
}
