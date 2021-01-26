// Dummy Post data by Sam Edwards
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;

// Next time I write tests, keep the data unique to each Controller and Repo
namespace Tabloid_Fullstack.Tests.PostTests
{
    public class PostDummyData
    {
        public static void GenerateData(TestDbContext _context)
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
                UserTypeId = 1
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

            var user4 = new UserProfile()
            {
                FirebaseUserId = "FirebaseIdAdmin",
                DisplayName = "Admin Big Jim",
                FirstName = "Big",
                LastName = "Jim",
                Email = "jim@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                ImageLocation = null,
                UserTypeId = 1
            };

            var user5 = new UserProfile()
            {
                FirebaseUserId = "FirebaseIdOwner",
                DisplayName = "Owner ",
                FirstName = "Big",
                LastName = "Jim",
                Email = "jim@ween.com",
                CreateDateTime = DateTime.Now - TimeSpan.FromDays(365),
                ImageLocation = null,
                UserTypeId = 2
            };

            _context.Add(user1);
            _context.Add(user2);
            _context.Add(user3);
            _context.Add(user4);
            _context.Add(user5);
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
