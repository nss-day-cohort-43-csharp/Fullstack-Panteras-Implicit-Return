using System.Collections.Generic;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetAll();
        //void Add(Comment comment);
        //void Update(Comment commnet);
        //void Delete(int id);
    }
}