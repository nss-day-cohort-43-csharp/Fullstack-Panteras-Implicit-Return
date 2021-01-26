using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Tabloid_Fullstack.Data;
using Tabloid_Fullstack.Models;
using Microsoft.AspNetCore.Http;

namespace Tabloid_Fullstack.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public UserProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            return _context.UserProfile
                .Include(up => up.UserType)
                .Include(up => up.Post)
                .FirstOrDefault(up => up.FirebaseUserId == firebaseUserId);

        }

        public UserProfile GetByUserProfileId(int id)
        {
            UserProfile user = _context.UserProfile
                .FirstOrDefault(uid => uid.Id == id);
            return user;
        }

        public void Add(UserProfile userProfile)
        {
            _context.Add(userProfile);
            _context.SaveChanges();
        }

        public void AddImageProfile(string image, int id)
        {
            var user = GetByUserProfileId(id);
            user.ImageLocation = image;
        }




    }
}
