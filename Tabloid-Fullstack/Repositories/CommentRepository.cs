using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Data;

namespace Tabloid_Fullstack.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Comment> GetAll(int PostId)
        {
            {
                return _context.Comment
                    .Where(c => c.PostId == PostId)
                    .OrderBy(c => c.CreateDateTime)
                    .ToList();
            }
            //throw new NotImplementedException();
        }
    }
}
