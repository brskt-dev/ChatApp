using Microsoft.EntityFrameworkCore;
using ChatApp.Domain.Entities;

namespace ChatApp.Infrastructure
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Message>().HasKey(m => m.Id);
        }
    }
}