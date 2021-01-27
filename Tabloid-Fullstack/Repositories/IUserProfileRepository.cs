using System;
using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        void AddImageProfile(Image image, int id);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetByUserProfileId(int id);
    }
}