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
            // Whenever GetById is invoked, with PostId of 1 passed in it, always return this post obj
            _fakePostRepository.Setup(r => r.GetById(It.Is<int>(i => i == 1))).Returns((int id) => new Post() { Id = id, UserProfileId = 2, Title = "Fake Title" });
            // Whenever GetById(2) is run, return null because it's not in our 'fake' database
            _fakePostRepository.Setup(r => r.GetById(It.Is<int>(i => i == 2))).Returns((int id) => null);

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
        }

        [Fact]
        public void Update_For_Only_Post_Owner()
        {
            // As an Owner, I should only be able to update my posts

            // My Post Id
            var postId = 1;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
            };

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOwner"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            controller.ControllerContext = new ControllerContext(); // Required to create the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user }; // Pretend the user is making a request to the controller

            // Attempt to Update the fake Post
            var response = controller.Put(postId, postToUpdate);

            // Owner should be able to update post
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void Update_Not_Allowed_For_NonAdmin_NonOwners()
        {
            // As a non-admin, non-owner, I should not be allowed to update a post

            // Not My Post Id
            var postId = 1;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
            };

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOther"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            controller.ControllerContext = new ControllerContext(); // Required to create the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user }; // Pretend the user is making a request to the controller

            // Attempt to Update the fake Post
            var response = controller.Put(postId, postToUpdate);

            // Non-admin, non-owner should get a NotFound response
            Assert.IsType<NotFoundResult>(response);
            // Verify that Update never gets called
            _fakePostRepository.Verify(r => r.Update(It.IsAny<Post>()), Times.Never());
        }

        [Fact]
        public void Update_For_Only_Matching_IdParam_And_PostId()
        {
            // Id from URL param and incoming Post Obj should match

            var postId = 1;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
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

            // Update should succeed because IdParam and PostId match
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void Update_For_Not_Matching_IdParam_And_PostId()
        {
            // If the Id from the URL parameter and incoming Post Obj do not match, return BadRequest
            // PostId that doesn't match
            var postId = 999999;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
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

            // Update should succeed because IdParam and PostId match
            Assert.IsType<BadRequestResult>(response);
            // Verify that Update never gets called
            _fakePostRepository.Verify(r => r.Update(It.IsAny<Post>()), Times.Never());
        }

        [Fact]
        public void Update_For_Only_Post_In_Db()
        {
            // The Id from the URL parameter must return a Post from the Db
            var postId = 1;
            var postToUpdate = new Post()
            {
                Id = 1,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
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

            // Update should succeed because IdParam and PostId match
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void Update_Returns_NotFound_If_Post_Is_Not_In_Db()
        {
            // The Id from the URL parameter returns null because it's not in Db
            var postId = 2; // 2 always returns Null
            var postToUpdate = new Post()
            {
                Id = 2,
                Title = "Fake Title",
                UserProfileId = 2 // This is the Owner's Post
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

            // Update should succeed because IdParam and PostId match
            Assert.IsType<NotFoundResult>(response);
            // Verify that Update never gets called
            _fakePostRepository.Verify(r => r.Update(It.IsAny<Post>()), Times.Never());
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

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            // Required to create the controller
            controller.ControllerContext = new ControllerContext();
            // Pretend the user is making a request to the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Attempt to delete a as Admin and not the Post Owner
            var response = controller.Delete(postId);

            // NoContent Result should return, meaning controller was success
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void Delete_For_Post_Owner()
        {
            // As the Post Owner, but not an Admin, I should be able to delete my own post.

            // Get a postId that is mine
            var postId = 1;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOwner"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            // Attempt to delete as a post Owner
            var response = controller.Delete(postId);

            // NoContent should return, meaning controller was success
            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void No_Delete_For_NonOwner_NonAdmin()
        {
            // As a non-owner, non-admin, I should not be able to delete somone's post

            // Get a postId that is mine
            var postId = 1;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdOther"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var response = controller.Delete(postId);

            // NotFound should return for Other
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public void Return_NotFound_For_No_Post_With_That_Id()
        {
            // If I try to delete a non-existent Post, even as an Admin, return NotFound

            // Get a postId that is null
            var postId = 2;

            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
                                   }, "TestAuthentication"));

            // Spoof the Post Controller
            var controller = new PostController(_fakePostRepository.Object, _fakeUserProfileRepository.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var response = controller.Delete(postId);

            // NotFound should return for a Null post Id
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
