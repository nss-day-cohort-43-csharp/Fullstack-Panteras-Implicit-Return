using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;
using Tabloid_Fullstack.Controllers.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TagController : ControllerBase
    {
     
        private ITagRepository _tagRepository;
        private IUserProfileRepository _userProfileRepository;

        public TagController(ITagRepository tagRepository, IUserProfileRepository userProfileRepository)
        {
            _tagRepository = tagRepository;
            _userProfileRepository = userProfileRepository;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var tags = _tagRepository.GetAll();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tag = _tagRepository.GetById(id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
       
        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            var storedUser = ControllerUtils.GetCurrentUserProfile(_userProfileRepository, User);
            if (storedUser.UserTypeId != 1)
            {
                return NotFound();
            }
            _tagRepository.Add(tag);
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            var exisitingPost = _tagRepository.GetById(id);

            if (exisitingPost == null)
            {
                return NotFound();
            }

            _tagRepository.Update(tag);
            return NoContent();
        }
    }
}
