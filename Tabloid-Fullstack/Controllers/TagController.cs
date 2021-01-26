//Authored by Terra Roush

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
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

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
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile.UserTypeId != 1)
            {
                return NotFound();
            }
            _tagRepository.Add(tag);
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Tag tag)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile.UserTypeId != 1)
            {
                return NotFound();
            }
            if (id != tag.Id)
            {
                return BadRequest();
            }

            var exisitingTag = _tagRepository.GetById(id);

            if (exisitingTag == null)
            {
                return NotFound();
            }
            exisitingTag.Name = tag.Name;

            _tagRepository.Update(exisitingTag);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile.UserTypeId != 1)
            {
                return NotFound();
            }
            var tag = _tagRepository.GetById(id);

            if (tag == null)
            {
                return NotFound();
            }

            _tagRepository.Delete(id);
            return NoContent();
        }
    }
}
