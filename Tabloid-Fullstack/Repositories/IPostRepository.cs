using System.Collections.Generic;
using Tabloid_Fullstack.Models;
using Tabloid_Fullstack.Models.ViewModels;

namespace Tabloid_Fullstack.Repositories
{
    public interface IPostRepository
    {
        List<PostSummary> Get();
        
        //GetByUserId by Sam Edwards
        List<PostSummary> GetByUserId(int userId);
        Post GetById(int id);
        List<ReactionCount> GetReactionCounts(int postId);
        //Add by Sam Edwards
        void Add(Post post);
        //Delete by Sam Edwards
        void Delete(int id);
    }
}