using System.Collections.Generic;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface ICommentRepository
    {
        public List<Comment> GetAllByPostId(int PostId);
        public List<Comment> GetAll();
        public void Add(Comment comment);
        //public void Update(Comment commnet);
        //public void Delete(int id);
    }
}