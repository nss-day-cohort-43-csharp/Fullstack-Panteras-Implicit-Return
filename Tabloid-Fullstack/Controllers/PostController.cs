using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Models.ViewModels;
using Tabloid_Fullstack.Repositories;
using Tabloid_Fullstack.Controllers.Utils;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private IPostRepository _repo;
        private IUserProfileRepository _userRepo;

        public PostController(IPostRepository repo, IUserProfileRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var posts = _repo.Get();
            return Ok(posts);
        }

        //GetByUserId by Sam Edwards
        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId()
        {
            // We are able to get the UserProfile because in this GET request, the
            // useEffect from the React client sends the JSON Web Token (JWT), which we're able to access
            // from the User ClaimsPrincipal object

            // If they are not logged in, they never make it this far into the method
            // so no need to have a try/catch or if statement for if it returns null
            var firebaseUser = ControllerUtils.GetCurrentUserProfile(_userRepo, User);
            var posts = _repo.GetByUserId(firebaseUser.Id);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var firebaseUser = ControllerUtils.GetCurrentUserProfile(_userRepo, User);
            var post = _repo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.IsApproved == false && firebaseUser.UserTypeId != 1 && post.UserProfileId != firebaseUser.Id)
            {
                return NotFound();
            }

            var reactionCounts = _repo.GetReactionCounts(id);
            var postDetails = new PostDetails()
            {
                Post = post,
                ReactionCounts = reactionCounts
            };
            return Ok(postDetails);
        }
    }
}
