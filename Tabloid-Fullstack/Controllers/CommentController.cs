using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ICommentRepository _commentRepository;
        private IUserProfileRepository _userRepo;

        public CommentController(ICommentRepository commentRepository, IUserProfileRepository userRepo)
        {
            _commentRepository = commentRepository;
            _userRepo = userRepo;
        }

        [HttpGet("{PostId}")]
        public IActionResult Get(int PostId)
        {
            return Ok(_commentRepository.GetAllByPostId(PostId));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_commentRepository.GetAll());
        }


        // GET: CommentController/Create
        [HttpPost]
        public IActionResult Add(Comment comment)
        {
            var currentUser = GetCurrentUserProfile();

            if (currentUser.UserTypeId != UserType.ADMIN_ID)
            {
                return Unauthorized();
            }
            comment.CreateDateTime = DateTime.Now;
            comment.UserProfileId = GetCurrentUserProfile().Id;

            _commentRepository.Add(comment);
            return Ok(comment);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //return _userRepo.GetByFirebaseUserId(firebaseUserId);
            UserProfile user = _userRepo.GetByFirebaseUserId(firebaseUserId);
            return user;
        }

    }
}