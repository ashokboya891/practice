

using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<AppUser> Users{get; set;}
        public DbSet<UserLike> Likes{get;set;}
        public DbSet<Message> Messages{get;set;}


         protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>().HasKey(k=>new {k.SourceUserId,k.TargetUserId});

            builder.Entity<UserLike>()
            .HasOne(s=>s.SourceUser)
            .WithMany(u=>u.LikedUsers)
            .HasForeignKey(s=>s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
            .HasOne(s=>s.TargetUser)
            .WithMany(u=>u.LikedByUsers)
            .HasForeignKey(s=>s.TargetUserId)
            .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<Message>()
            .HasOne(s=>s.Recipient)
            .WithMany(u=>u.MessageReceived)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
            .HasOne(s=>s.Sender)
            .WithMany(u=>u.MessageSent)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}