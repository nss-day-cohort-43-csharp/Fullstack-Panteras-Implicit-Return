using Tabloid_Fullstack.Models;

namespace Tabloid_Fullstack.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetByUserProfileId(int id);
    }
}