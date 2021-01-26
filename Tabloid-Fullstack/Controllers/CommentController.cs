using System;
using System.Collections.Generic;
using System.Linq;
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

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
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
            _commentRepository.Add(comment);
            return Ok(comment);
        }

    }
}