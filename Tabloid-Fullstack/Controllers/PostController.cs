using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Models.ViewModels;
using Tabloid_Fullstack.Repositories;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepo.GetByFirebaseUserId(firebaseUserId);
        }
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var firebaseUser = GetCurrentUserProfile();
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
