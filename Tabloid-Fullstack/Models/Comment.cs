﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tabloid_Fullstack.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public UserProfile UserProfile { get; set; }

        public Post Post { get; set; }
        public int UserProfileId { get; set; }

        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }

    }
}