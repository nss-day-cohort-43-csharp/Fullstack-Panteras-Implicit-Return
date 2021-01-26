using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tabloid_Fullstack.Data;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Comment> GetAllByPostId(int PostId)
        {
            {
                return _context.Comment
                    .Where(c => c.PostId == PostId)
                    .OrderBy(c => c.CreateDateTime)
                    .Include(c => c.UserProfile)
                    .ToList();
            }
        }

        public List<Comment> GetAll()
        {
            {
                return _context.Comment
                    .OrderBy(c => c.CreateDateTime)
                    .ToList();
            }
        }

        public void Add(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
        }
    }
}