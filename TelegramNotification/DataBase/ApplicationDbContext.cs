using Microsoft.EntityFrameworkCore;
using TelegramNotification.DataBase.Model;

namespace TelegramNotification.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TelegramId)
                    .IsRequired();
                
                entity.Property(e => e.NickName)
                    .IsRequired();
                entity.HasIndex(e => e.NickName)
                    .IsUnique();
                

                entity.Property(e => e.CreationDate)
                    .IsRequired();

                entity.HasOne(u => u.UserSettings)
                    .WithOne(us => us.User)
                    .HasForeignKey<UserSettings>(us => us.UserId);  
            });
            
            modelBuilder.Entity<UserSettings>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.IsEnableNotification)
                    .HasDefaultValue(true);
            });
        }
    }
}