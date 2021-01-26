using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tabloid_Fullstack.Controllers;
using Tabloid_Fullstack.Repositories;
using Xunit;

namespace Tabloid_Fullstack.Tests.PostTests
{
    public class PostControllerTests : EFTestFixture
    {
        public PostControllerTests()
        {
            // When constructed, generate dummy data, passing in EFTestFixture database
            PostDummyData.GenerateData(_context);
        }

        [Fact]
        public void Delete_For_Only_Admin()
        {
            // As an Admin, I should be able to delete any post, including those that aren't mine

            // Get a postId that is not mine
            var postId = 1;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
                                   }, "TestAuthentication"));

            // Spoof the PostRepository and UserProfile Repository to create a PostController
            var postRepo = new PostRepository(_context);
            var userProfileRepo = new UserProfileRepository(_context);

            // Get full count of posts
            var totalPostCount = postRepo.Get().Count;

            // Spoof the Post Controller
            var controller = new PostController(postRepo, userProfileRepo);
            // Required to create the controller
            controller.ControllerContext = new ControllerContext();
            // Pretend the user is making a request to the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Attempt to delete a as Admin and not the Post Owner
            // I do not need to pass in the user
            // it's already in the fake HttpContext in the ControllerContext
            controller.Delete(postId);

            var totalPostCountAfterDeletion = postRepo.Get().Count;

            // TotalPostCountAfterDeletion should be one less than totalPostCount
            Assert.True(totalPostCountAfterDeletion == totalPostCount - 1);
        }

        [Fact]
        public void Delete_For_Post_Owner()
        {
            // As the Post Owner, but not an Admin, I should be able to delete my own post.

            // Get a postId that is mine
            var postId = 1;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "TEST_FIREBASE_UID_3"),
                                   }, "TestAuthentication"));

            // Spoof the PostRepository and UserProfile Repository to create a PostController
            var postRepo = new PostRepository(_context);
            var userProfileRepo = new UserProfileRepository(_context);

            // Get full count of posts
            var totalPostCount = postRepo.Get().Count;

            // Spoof the Post Controller
            var controller = new PostController(postRepo, userProfileRepo);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            controller.Delete(postId);

            var totalPostCountAfterDeletion = postRepo.Get().Count;

            // TotalPostCountAfterDeletion should be one less than totalPostCount
            Assert.True(totalPostCountAfterDeletion == totalPostCount - 1);
        }

        [Fact]
        public void No_Delete_For_NonOwner_NonAdmin()
        {
            // As a non-owner, non-admin, I should not be able to delete somone's post

            // Get a postId that is mine
            var postId = 1;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOwner"),
                                   }, "TestAuthentication"));

            // Spoof the PostRepository and UserProfile Repository to create a PostController
            var postRepo = new PostRepository(_context);
            var userProfileRepo = new UserProfileRepository(_context);

            // Get full count of posts
            var totalPostCount = postRepo.Get().Count;

            // Spoof the Post Controller
            var controller = new PostController(postRepo, userProfileRepo);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            controller.Delete(postId);

            var totalPostCountAfterDeletion = postRepo.Get().Count;

            // TotalPostCountAfterDeletion should be one less than totalPostCount
            Assert.True(totalPostCountAfterDeletion == totalPostCount);
        }

        [Fact]
        public void Return_NotFound_For_No_Post_With_That_Id()
        {
            // If I try to delete a non-existent Post, even as an Admin, return NotFound

            // Get a postId that is non-existent
            var postId = 99999999;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
                                   }, "TestAuthentication"));

            // Spoof the PostRepository and UserProfile Repository to create a PostController
            var postRepo = new PostRepository(_context);
            var userProfileRepo = new UserProfileRepository(_context);

            // Spoof the Post Controller
            var controller = new PostController(postRepo, userProfileRepo);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            StatusCodeResult response = (StatusCodeResult)controller.Delete(postId);

            // TotalPostCountAfterDeletion should be one less than totalPostCount
            Assert.True(response.StatusCode == 404);
        }
    }
}
