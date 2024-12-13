using DatingApp.Models;

namespace DatingApp.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
        //    if (!context.Users.Any())
        //    {
        //        // Create test users
        //        var users = new List<AppUser>
        //        {
        //            new AppUser
        //            {
        //                Email = "john@test.com",
        //                Password = "password123", // In real app, hash this!
        //                Name = "John Doe",
        //                BirthDate = new DateTime(1990, 1, 1),
        //                Location = "Amsterdam",
        //                UserTypeId = (int)UserType.UserTypeEnum.User,
        //                IsActive = true,
        //                CreatedAt = DateTime.UtcNow,
        //                Photos = new[] { "photo1.jpg" }
        //            },
        //            new AppUser
        //            {
        //                Email = "jane@test.com",
        //                Password = "password123",
        //                Name = "Jane Smith",
        //                BirthDate = new DateTime(1992, 5, 15),
        //                Location = "Rotterdam",
        //                UserTypeId = (int)UserType.UserTypeEnum.User,
        //                IsActive = true,
        //                CreatedAt = DateTime.UtcNow,
        //                Photos = new[] { "photo2.jpg" }
        //            },
        //            new AppUser
        //            {
        //                Email = "admin@test.com",
        //                Password = "admin123",
        //                Name = "Admin User",
        //                BirthDate = new DateTime(1985, 12, 25),
        //                Location = "Utrecht",
        //                UserTypeId = (int)UserType.UserTypeEnum.Admin,
        //                IsActive = true,
        //                CreatedAt = DateTime.UtcNow,
        //                Photos = new[] { "admin.jpg" }
        //            }
        //        };

        //        context.Users.AddRange(users);
        //        context.SaveChanges();

        //        // Create profiles for users
        //        var profiles = new List<Profile>
        //        {
        //            new Profile
        //            {
        //                UserId = users[0].UserId,
        //                Bio = "I love hiking and photography",
        //                Gender = "Male",
        //                Preferences = new[] { "Female" },
        //                MinAge = 25,
        //                MaxAge = 35,
        //                LastActive = DateTime.UtcNow
        //            },
        //            new Profile
        //            {
        //                UserId = users[1].UserId,
        //                Bio = "Love to travel and meet new people",
        //                Gender = "Female",
        //                Preferences = new[] { "Male" },
        //                MinAge = 27,
        //                MaxAge = 38,
        //                LastActive = DateTime.UtcNow
        //            }
        //        };

        //        context.Profiles.AddRange(profiles);
        //        context.SaveChanges();

        //        // After creating profiles, add quizzes
        //        var quizzes = new List<Quiz>
        //        {
        //            new Quiz
        //            {
        //                UserId = users[0].UserId,
        //                AgePreference = "25-35",
        //                RelationshipType = "Long-term",
        //                SportImportance = 8,
        //                SocialLevel = 7,
        //                WeekendActivity = "Outdoor activities",
        //                CompletedAt = DateTime.UtcNow.AddDays(-5)
        //            },
        //            new Quiz
        //            {
        //                UserId = users[1].UserId,
        //                AgePreference = "27-40",
        //                RelationshipType = "Long-term",
        //                SportImportance = 6,
        //                SocialLevel = 8,
        //                WeekendActivity = "Mix of indoor and outdoor",
        //                CompletedAt = DateTime.UtcNow.AddDays(-3)
        //            }
        //        };

        //        context.Quizzes.AddRange(quizzes);
        //        context.SaveChanges();

        //        // Add some dates
        //        var dates = new List<Date>
        //        {
        //            new Date
        //            {
        //                UserId = users[0].UserId,
        //                DateUserId = users[1].UserId,
        //                Location = "Coffee Corner Amsterdam",
        //                DateTime = DateTime.UtcNow.AddDays(2),
        //                Status = "Planned"
        //            },
        //            new Date
        //            {
        //                UserId = users[0].UserId,
        //                DateUserId = users[1].UserId,
        //                Location = "Vondelpark",
        //                DateTime = DateTime.UtcNow.AddDays(-5),
        //                Status = "Completed"
        //            }
        //        };

        //        context.Dates.AddRange(dates);
        //        context.SaveChanges();

        //        // Add reviews for completed date
        //        var reviews = new List<Review>
        //        {
        //            new Review
        //            {
        //                UserId = users[0].UserId,
        //                ReviewedUserId = users[1].UserId,
        //                Rating = 5,
        //                Comment = "Had a great time! Very nice person and easy to talk to.",
        //                CreatedAt = DateTime.UtcNow.AddDays(-4)
        //            },
        //            new Review
        //            {
        //                UserId = users[1].UserId,
        //                ReviewedUserId = users[0].UserId,
        //                Rating = 4,
        //                Comment = "Really enjoyed our conversation and shared interests.",
        //                CreatedAt = DateTime.UtcNow.AddDays(-4)
        //            }
        //        };

        //        context.Reviews.AddRange(reviews);
        //        context.SaveChanges();

        //        // Create a match between users
        //        var match = new Match
        //        {
        //            UserId = users[0].UserId,
        //            MatchedUserId = users[1].UserId,
        //            MatchPercentage = 85.5f,
        //            Status = "Pending",
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        context.Matches.Add(match);
        //        context.SaveChanges();

        //        // Create a chat between matched users
        //        var chat = new Chat
        //        {
        //            UserId = users[0].UserId,
        //            ChatUserId = users[1].UserId,
        //            Status = "Active",
        //            LastMessage = DateTime.UtcNow
        //        };

        //        context.Chats.Add(chat);
        //        context.SaveChanges();

        //        // Add some messages to the chat
        //        var messages = new List<Message>
        //        {
        //            new Message
        //            {
        //                ChatId = chat.ChatId,
        //                Content = "Hi, how are you?",
        //                Timestamp = DateTime.UtcNow.AddMinutes(-30),
        //                IsRead = true
        //            },
        //            new Message
        //            {
        //                ChatId = chat.ChatId,
        //                Content = "I'm good, thanks! How about you?",
        //                Timestamp = DateTime.UtcNow.AddMinutes(-25),
        //                IsRead = true
        //            }
        //        };

        //        context.Messages.AddRange(messages);
        //        context.SaveChanges();
        //    }
        }
    }
} 