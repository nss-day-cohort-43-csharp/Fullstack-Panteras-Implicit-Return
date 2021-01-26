﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tabloid_Fullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepo;
        private IUserProfileRepository _userRepo;

        public CategoryController(ICategoryRepository categoryRepo, IUserProfileRepository userRepo)
        {
            _categoryRepo = categoryRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryRepo.Get();
            return Ok(categories);
        }

        [HttpPost]
        public IActionResult Post(Category category)
        {
            var currentUser = GetCurrentUserProfile();

            if (currentUser.UserTypeId != UserType.ADMIN_ID)
            {
                return Unauthorized();
            }

            _categoryRepo.Add(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userRepo.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
