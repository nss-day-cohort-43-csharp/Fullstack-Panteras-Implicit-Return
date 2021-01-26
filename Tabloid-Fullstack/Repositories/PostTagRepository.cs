using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tabloid_Fullstack.Data;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private ApplicationDbContext _context;
        public PostTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PostTag> GetAll()
        {
            return _context.PostTag.ToList();
        }

        public PostTag GetById(int id)
        {
            return _context.PostTag
                .Where(pt => pt.Id == id)
                .FirstOrDefault();
        }

        public void Add(PostTag postTag)
        {
            _context.Add(postTag);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var postTag = GetById(id);
            _context.PostTag.Remove(postTag);
            _context.SaveChanges();
        }
    }
}
