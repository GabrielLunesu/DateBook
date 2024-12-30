using Microsoft.EntityFrameworkCore;
using DatingApp.Models;
using dating_app_server.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Text.Json;

namespace DatingApp.Data
{
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuizResponse> QuizResponses { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }



        public DbSet<ProfileQuestion> ProfileQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
          .HasMany(ur => ur.UserRoles)
          .WithOne(u => u.User)
          .HasForeignKey(ur => ur.UserId)
          .IsRequired();

            modelBuilder.Entity<AppUser>()
    .Property(u => u.Photos)
    .HasConversion(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => JsonSerializer.Deserialize<string[]>(v, (JsonSerializerOptions)null)
    );

        //    modelBuilder.Entity<AppUser>()
        //.Property(u => u.Photos)
        //.HasConversion(
        //    v => string.Join(",", v),          // Convert array to string for saving
        //    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries) // Convert string back to array
        //);


            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            // UserType - User (1:Many)
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Profile (1:1)
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - ProfileQuestion (1:Many)
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Questions)
                .WithOne(pq => pq.User)
                .HasForeignKey(pq => pq.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Quiz (1:1)
            // modelBuilder.Entity<AppUser>()
            //     .HasOne(u => u.Quiz)
            //     .WithOne(q => q.User)
            //     .HasForeignKey<QuizResponse>(q => q.UserId)
            //     .OnDelete(DeleteBehavior.Cascade);

            // User - Match (1:Many)
            modelBuilder.Entity<Match>()
                .HasOne(m => m.User)
                .WithMany(u => u.Matches)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.MatchedUser)
                .WithMany()
                .HasForeignKey(m => m.MatchedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Date (1:Many)
            modelBuilder.Entity<Date>()
                .HasOne(d => d.User)
                .WithMany(u => u.Dates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Date>()
                .HasOne(d => d.DateUser)
                .WithMany()
                .HasForeignKey(d => d.DateUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Review (1:Many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.ReviewedUser)
                .WithMany()
                .HasForeignKey(r => r.ReviewedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Chat (1:Many)
            modelBuilder.Entity<Chat>()
                .HasOne(c => c.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.ChatUser)
                .WithMany()
                .HasForeignKey(c => c.ChatUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chat - Message (1:Many)
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure array properties
            modelBuilder.Entity<AppUser>()
                .Property(u => u.Photos)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<Profile>()
                .Property(p => p.Preferences)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            modelBuilder.Entity<QuizResponse>()
                .HasOne(q => q.User)
                .WithMany(u => u.QuizResponses)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuizResponse>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.QuizResponses) // Assuming a Quiz can have multiple QuizResponses
                .HasForeignKey(q => q.QuizId);

            // Seed initial UserTypes
            modelBuilder.Entity<UserType>().HasData(
                new UserType { TypeId = (int)UserType.UserTypeEnum.User, Name = "User" },
                new UserType { TypeId = (int)UserType.UserTypeEnum.Moderator, Name = "Moderator" },
                new UserType { TypeId = (int)UserType.UserTypeEnum.Admin, Name = "Admin" }
            );
        }
    }
}