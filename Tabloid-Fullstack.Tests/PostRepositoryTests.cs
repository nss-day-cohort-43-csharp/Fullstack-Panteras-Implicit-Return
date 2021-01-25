// Repository Unit Tests by Sam Edwards
using System;
using Tabloid_Fullstack.Models;
using Xunit;

// We are able to do these tests because we "spoof" the ApplicationDbContext using Entity Framework and the EFTextFixture Class
// For each test, we create a new database in memory. Add some seed data, run our test, then delete Db.
// This way we have total control of our test environment.
namespace Tabloid_Fullstack.Tests
{
    // Inherits the EFTestFixture so it has access to our fake database 
    public class PostRepositoryTests : EFTestFixture
    {

        public PostRepositoryTests()
        {
            AddSampleData();
        }

        // Add sample data
        private void AddSampleData()
        {
            var user1 = new UserProfile()
            {
                DisplayName = "Dean",
                FirstName = "Michael",
                LastName = "Melchiondo",
                Email = "dean@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                FirebaseUserId = "TEST_FIREBASE_UID_1"
            };

            var user2 = new UserProfile()
            {
                DisplayName = "Gene",
                FirstName = "Aaron",
                LastName = "Freeman",
                Email = "gene@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                FirebaseUserId = "TEST_FIREBASE_UID_2"
            };

            var user3 = new UserProfile()
            {
                DisplayName = "bestDrummer",
                FirstName = "Claude",
                LastName = "Coleman",
                Email = "claude@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                FirebaseUserId = "TEST_FIREBASE_UID_3"
            };

            _context.Add(user1);
            _context.Add(user2);
            _context.Add(user3);

            var post1 = new Post()
            {
                Title = "Voodoo Lady",
                Content = "One of the best songs on Chocolate & Cheese",
                ImageLocation = "http://foo.gif",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(10),
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 1,
                UserProfileId = 1
            };

            var post2 = new Post()
            {
                Title = "Ocean Man",
                Content = "The song everyone knows Ween for",
                ImageLocation = "http://foo.gif",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(10),
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 2
            };

            var post3 = new Post()
            {
                Title = "Exactly Where I'm At",
                Content = "First song from White Pepper. Starts the album off right.",
                ImageLocation = "http://foo.gif",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(10),
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 3,
                UserProfileId = 3
            };

            _context.Add(post1);
            _context.Add(post2);
            _context.Add(post3);
            _context.SaveChanges();
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
