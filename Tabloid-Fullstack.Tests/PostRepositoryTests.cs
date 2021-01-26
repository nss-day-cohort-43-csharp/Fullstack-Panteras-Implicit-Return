// Repository Unit Tests by Sam Edwards
using System;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;
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
            // When constructed, runs dummy data
            AddSampleData();
        }

        [Fact]
        public void User_Can_Get_List_Of_Their_Posts()
        {
            var userId = 1;
            var repo = new PostRepository(_context);

            // Attempt to get list of user's posts
            var usersPosts = repo.GetByUserId(userId);

            // We should have 2 posts, with the AuthorId's matching userId
            Assert.True(usersPosts.Count == 2);
            Assert.True(usersPosts[0].AuthorId == userId);
            Assert.True(usersPosts[1].AuthorId == userId);
        }

        [Fact]
        public void User_Can_Add_Post()
        {
            // Create a new Post, leaving out the CreateDateTime because that gets created on Add
            var post = new Post
            {
                Title = "Ween, a band, that's really good",
                Content = "Everyone should listen to Ween. They're a pretty fun band. The End.",
                ImageLocation = "http://foo.gif",
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 3
            };

            // Get our PostRepo
            var repo = new PostRepository(_context);

            // Get count of all posts
            var originalPostAmount = repo.Get().Count;

            // Attempt to Add Post
            repo.Add(post);

            // Get post count after addition
            var afterPostAddAmount = repo.Get().Count;

            // We should have more posts than before
            Assert.True(afterPostAddAmount > originalPostAmount);
        }


        // Add sample data
        private void AddSampleData()
        {
            var userType1 = new UserType()
            {
                Name = "admin"
            };

            var userType2 = new UserType()
            {
                Name = "owner"
            };

            _context.Add(userType1);
            _context.Add(userType2);
            _context.SaveChanges();

            var user1 = new UserProfile()
            {
                FirebaseUserId = "TEST_FIREBASE_UID_1",
                DisplayName = "Dean",
                FirstName = "Michael",
                LastName = "Melchiondo",
                Email = "dean@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                ImageLocation = null,
                UserTypeId =  1
            };

            var user2 = new UserProfile()
            {
                FirebaseUserId = "TEST_FIREBASE_UID_2",
                DisplayName = "Gene",
                FirstName = "Aaron",
                LastName = "Freeman",
                Email = "gene@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                ImageLocation = null,
                UserTypeId = 2
            };

            var user3 = new UserProfile()
            {
                FirebaseUserId = "TEST_FIREBASE_UID_3",
                DisplayName = "bestDrummer",
                FirstName = "Claude",
                LastName = "Coleman",
                Email = "claude@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                ImageLocation = null,
                UserTypeId = 2
            };

            _context.Add(user1);
            _context.Add(user2);
            _context.Add(user3);
            _context.SaveChanges();

            var category1 = new Category()
            {
                Name = "Best Ween Songs"
            };

            var category2 = new Category()
            {
                Name = "Most Underrated Ween Songs"
            };

            _context.Add(category1);
            _context.Add(category2);
            _context.SaveChanges();

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
                UserProfileId = 1
            };

            var post3 = new Post()
            {
                Title = "Exactly Where I'm At",
                Content = "First song from White Pepper. Starts the album off right.",
                ImageLocation = "http://foo.gif",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(10),
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 3
            };

            _context.Add(post1);
            _context.Add(post2);
            _context.Add(post3);
            _context.SaveChanges();
        }
    }
}
