// Repository Unit Tests by Sam Edwards
using Microsoft.AspNetCore.Mvc;
using System;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;
using Xunit;

// We are able to do these tests because we "spoof" the ApplicationDbContext using Entity Framework and the EFTextFixture Class
// For each test, we create a new database in memory. Add some seed data, run our test, then delete Db.
// This way we have total control of our test environment.
namespace Tabloid_Fullstack.Tests.PostTests
{
    // Inherits the EFTestFixture so it has access to our fake database 
    public class PostRepositoryTests : EFTestFixture
    {

        public PostRepositoryTests()
        {
            // When constructed, generate dummy data, passing in EFTestFixture database
            PostDummyData.GenerateData(_context);
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

        [Fact]
        public void User_Entered_Valid_ImageLocation()
        {
            // Create a new Post with a good ImageLocation
            var post = new Post
            {
                Title = "Ween, a band, that's really good",
                Content = "Everyone should listen to Ween. They're a pretty fun band. The End.",
                ImageLocation = "https://en.wikipedia.org/wiki/Ween#/media/File:Ween_(1993).jpg",
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 3
            };

            // Get our PostRepo
            var repo = new PostRepository(_context);

            // Add Post
            repo.Add(post);

            // Get all our posts, then the last post
            var posts = repo.Get();
            var justAddedPost = posts[0];

            // The post we just added should have same URL as the above post
            Assert.True(justAddedPost.ImageLocation == post.ImageLocation);
        }

        [Fact]
        public void User_Entered_Invalid_ImageLocation()
        {
            // Create a new Post with a bad ImageLocation
            var post = new Post
            {
                Title = "Ween, a band, that's really good",
                Content = "Everyone should listen to Ween. They're a pretty fun band. The End.",
                ImageLocation = "Ween is like---totally---cool",
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 3
            };

            // Get our PostRepo
            var repo = new PostRepository(_context);

            // Add Post
            repo.Add(post);

            // Get all our posts, then the last post
            var posts = repo.Get();
            var justAddedPost = posts[0];

            // The post we just added should have the default URL string.
            Assert.True(justAddedPost.ImageLocation == "http://lorempixel.com/920/360/");
        }

        [Fact]
        public void User_Can_Delete_Post()
        {
            // Create a new Post to delete
            var post = new Post
            {
                Title = "Ween, a band, that's really good",
                Content = "Everyone should listen to Ween. They're a pretty fun band. The End.",
                ImageLocation = "Ween is like---totally---cool",
                PublishDateTime = DateTime.Now - TimeSpan.FromDays(10),
                IsApproved = true,
                CategoryId = 2,
                UserProfileId = 3
            };

            // Get our PostRepo
            var repo = new PostRepository(_context);

            // Add that Post to Db
            repo.Add(post);

            // Get a count of all posts
            var postTotal = repo.Get().Count;

            // Delete just added post
            repo.Delete(post);

            // Get a new count of all posts;
            var postTotalAfterDeletion = repo.Get().Count;

            // Post total after deletion should be one less than original total
            Assert.True(postTotalAfterDeletion == postTotal - 1);
        }


    }
}
