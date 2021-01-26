﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostTagController : ControllerBase
    {

        private IPostTagRepository _postTagRepository;
        private IUserProfileRepository _userProfileRepository;
        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

        public PostTagController(IPostTagRepository postTagRepository, IUserProfileRepository userProfileRepository)
        {
            _postTagRepository = postTagRepository;
            _userProfileRepository = userProfileRepository;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var postTags = _postTagRepository.GetAll();
            return Ok(postTags);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var postTag = _postTagRepository.GetById(id);
            if (postTag == null)
            {
                return NotFound();
            }
            return Ok(postTag);
        }

        [HttpPost]
        public IActionResult Add(PostTag postTag)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile.UserTypeId != 1)
            {
                return NotFound();
            }
            _postTagRepository.Add(postTag);
            return Ok(postTag);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var currentUserProfile = GetCurrentUserProfile();
            if (currentUserProfile.UserTypeId != 1)
            {
                return NotFound();
            }
            var postTag = _postTagRepository.GetById(id);

            if (postTag == null)
            {
                return NotFound();
            }

            _postTagRepository.Delete(id);
            return NoContent();
        }
    }
}
