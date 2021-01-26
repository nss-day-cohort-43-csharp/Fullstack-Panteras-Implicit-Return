using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tabloid_Fullstack.Controllers;
using Tabloid_Fullstack.Controllers.Utils;
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
            // Spoof an authenticated user by generating a ClaimsPrincipal
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "FirebaseIdAdmin"),
                                   }, "TestAuthentication"));

            // Spoof the PostRepository and UserProfile Repository to create a PostController
            var postRepo = new PostRepository(_context);
            var userProfileRepo = new UserProfileRepository(_context);

            // Spoof the Post Controller
            var controller = new PostController(postRepo, userProfileRepo);
            // Required to create the controller
            controller.ControllerContext = new ControllerContext();
            // Pretend the user is making a request to the controller
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var loggedInUser = ControllerUtils.GetCurrentUserProfile(userProfileRepo, user);

        }
    }
}
