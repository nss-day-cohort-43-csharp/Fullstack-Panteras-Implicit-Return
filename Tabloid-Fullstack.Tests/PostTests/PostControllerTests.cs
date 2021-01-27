using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tabloid_Fullstack.Controllers;
using Tabloid_Fullstack.Repositories;
using Moq;
using Xunit;
using Tabloid_Fullstack.Models;

// When doing Controller Unit Tests, you do NOT need to Spoof the Database because we ARE NOT
// Making any changes to it. We Spoof the Repositories. We are able to do this because they are Interfaces
namespace Tabloid_Fullstack.Tests.PostTests
{
    public class PostControllerTests
    {

        private Mock<IPostRepository> _fakePostRepository;
        private Mock<IUserProfileRepository> _fakeUserProfileRepository;

        public PostControllerTests()
        {
            // By Spoofing the return values from our Repos, we don't need a Db
            // Spoof Post Repository
            _fakePostRepository = new Mock<IPostRepository>();
            // Whenever GetById is invoked, with any passed in it, always return this post obj
            _fakePostRepository.Setup(r => r.GetById(It.IsAny<int>())).Returns((int id) => new Post() { Id = id, UserProfileId = 2, Title = "Fake Title" });

            // Spoof a UserProfileRepository
            _fakeUserProfileRepository = new Mock<IUserProfileRepository>();
            // Spoof a User with FirebaseId of an Owner, Admin, and Other
            _fakeUserProfileRepository.Setup(r => r.GetByFirebaseUserId("FirebaseIdAdmin")).Returns(new UserProfile() { Id = 1, DisplayName = "Admin", UserTypeId = 1 });
            _fakeUserProfileRepository.Setup(r => r.GetByFirebaseUserId("FirebaseIdOwner")).Returns(new UserProfile() { Id = 2, DisplayName = "DisplayName", UserTypeId = 2 });
            _fakeUserProfileRepository.Setup(r => r.GetByFirebaseUserId("FirebaseIdOther")).Returns(new UserProfile() { Id = 3, DisplayName = "Admin", UserTypeId = 2 });
        }

        [Fact]
        public void Update_For_Only_Admin()
        {
            // As an Admin, I should be able to update any posts, including those that aren't mine.
            // Get a postId that is not mine
            var postId = 1;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // Not the Admin's Post
            };

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object); 
            controller.ControllerContext = new ControllerContext(); // Required to create the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user }; // Pretend the user is making a request to the controller

            // Attempt to Update the fake Post
            var response = controller.Put(postId, postToUpdate);

            // Admin should be able to update post
            Assert.IsType<NoContentResult>(response);
            // Verifction that Update never gets called - PUT THIS FOR TESTS WE CAN NOT UPDATE
            //_fakePostRepository.Verify(r => r.Update(It.IsAny<Post>()), Times.Never());
        }

        [Fact]
        public void Update_For_Only_Post_Owner()
        {
            // As an Owner, I should only be able to update my posts
        }

        [Fact]
        public void Update_Not_Allowed_For_NonAdmin_NonOwners()
        {

        }

        [Fact]
        public void Update_For_Only_Matching_IdParam_And_PostId()
        {
            // If the Id from the URL parameter and the incoming Post Obj do not match, return BadRequest
        }

        [Fact]
        public void Update_For_Only_Post_In_Db()
        {
            // The Id from the URL parameter must return a Post from the Db, or return NotFound
        }

        [Fact]
        public void Update_For_Only_Posts_That_Have_Matching_Names()
        {
            // Names from the PostToEdit from the Db and incoming Post must match, else return BadRequest
        }

        //[Fact]
        //public void Delete_For_Only_Admin()
        //{
        //    // As an Admin, I should be able to delete any post, including those that aren't mine

        //    // Get a postId that is not mine
        //    var postId = 1;

        //    // Spoof an authenticated user by generating a ClaimsPrincipal
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        //                                new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
        //                           }, "TestAuthentication"));

        //    // Spoof the PostRepository and UserProfile Repository to create a PostController
        //    var postRepo = new PostRepository(_context);
        //    var userProfileRepo = new UserProfileRepository(_context);

        //    // Get full count of posts
        //    var totalPostCount = postRepo.Get().Count;

        //    // Spoof the Post Controller
        //    var controller = new PostController(postRepo, userProfileRepo);
        //    // Required to create the controller
        //    controller.ControllerContext = new ControllerContext();
        //    // Pretend the user is making a request to the controller
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

        //    // Attempt to delete a as Admin and not the Post Owner
        //    // I do not need to pass in the user
        //    // it's already in the fake HttpContext in the ControllerContext
        //    controller.Delete(postId);

        //    var totalPostCountAfterDeletion = postRepo.Get().Count;

        //    // TotalPostCountAfterDeletion should be one less than totalPostCount
        //    Assert.True(totalPostCountAfterDeletion == totalPostCount - 1);
        //}

        //[Fact]
        //public void Delete_For_Post_Owner()
        //{
        //    // As the Post Owner, but not an Admin, I should be able to delete my own post.

        //    // Get a postId that is mine
        //    var postId = 1;

        //    // Spoof an authenticated user by generating a ClaimsPrincipal
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        //                                new Claim(ClaimTypes.NameIdentifier, "TEST_FIREBASE_UID_3"),
        //                           }, "TestAuthentication"));

        //    // Spoof the PostRepository and UserProfile Repository to create a PostController
        //    var postRepo = new PostRepository(_context);
        //    var userProfileRepo = new UserProfileRepository(_context);

        //    // Get full count of posts
        //    var totalPostCount = postRepo.Get().Count;

        //    // Spoof the Post Controller
        //    var controller = new PostController(postRepo, userProfileRepo);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

        //    controller.Delete(postId);

        //    var totalPostCountAfterDeletion = postRepo.Get().Count;

        //    // TotalPostCountAfterDeletion should be one less than totalPostCount
        //    Assert.True(totalPostCountAfterDeletion == totalPostCount - 1);
        //}

        //[Fact]
        //public void No_Delete_For_NonOwner_NonAdmin()
        //{
        //    // As a non-owner, non-admin, I should not be able to delete somone's post

        //    // Get a postId that is mine
        //    var postId = 1;

        //    // Spoof an authenticated user by generating a ClaimsPrincipal
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        //                                new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOwner"),
        //                           }, "TestAuthentication"));

        //    // Spoof the PostRepository and UserProfile Repository to create a PostController
        //    var postRepo = new PostRepository(_context);
        //    var userProfileRepo = new UserProfileRepository(_context);

        //    // Get full count of posts
        //    var totalPostCount = postRepo.Get().Count;

        //    // Spoof the Post Controller
        //    var controller = new PostController(postRepo, userProfileRepo);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

        //    controller.Delete(postId);

        //    var totalPostCountAfterDeletion = postRepo.Get().Count;

        //    // TotalPostCountAfterDeletion should be one less than totalPostCount
        //    Assert.True(totalPostCountAfterDeletion == totalPostCount);
        //}

        //[Fact]
        //public void Return_NotFound_For_No_Post_With_That_Id()
        //{
        //    // If I try to delete a non-existent Post, even as an Admin, return NotFound

        //    // Get a postId that is non-existent
        //    var postId = 99999999;

        //    // Spoof an authenticated user by generating a ClaimsPrincipal
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
        //                                new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
        //                           }, "TestAuthentication"));

        //    // Spoof the PostRepository and UserProfile Repository to create a PostController
        //    var postRepo = new PostRepository(_context);
        //    var userProfileRepo = new UserProfileRepository(_context);

        //    // Spoof the Post Controller
        //    var controller = new PostController(postRepo, userProfileRepo);
        //    controller.ControllerContext = new ControllerContext();
        //    controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

        //    StatusCodeResult response = (StatusCodeResult)controller.Delete(postId);

        //    // TotalPostCountAfterDeletion should be one less than totalPostCount
        //    Assert.True(response.StatusCode == 404);
        //}

    }
}
