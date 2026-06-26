using Microsoft.EntityFrameworkCore;
using Todo.Entities;
using Task = Todo.Entities.Task;

namespace Todo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
               entity.HasKey(u => u.Id); 
               entity.HasIndex(u => u.Mail).IsUnique();
               entity.Property(u => u.Mail).HasMaxLength(100);

            });
            modelBuilder.Entity<Task>(entity =>
            {
               entity.HasKey(t => t.Id);
               entity.HasOne(u => u.User).WithMany(t => t.Tasks).HasForeignKey(t => t.UserId);
            });
        }

    }
}