using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models.ViewModels;
using Tabloid_Fullstack.Repositories;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {

        private IPostRepository _repo;

        public PostController(IPostRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var posts = _repo.Get();
            return Ok(posts);
        }

        //GetByUserId by Sam Edwards
        [HttpGet("getbyuserid/{id}")]
        public IActionResult GetByUserId(int id)
        {
            var posts = _repo.GetByUserId(id);
            return Ok(posts);
        }

        [HttpGet("{postId}/{userId}")]
        public IActionResult GetById(int id)
        {
            var post = _repo.GetById(id);
            if (post == null)
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
