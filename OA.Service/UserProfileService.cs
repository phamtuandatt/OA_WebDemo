using OA.Dataa;
using OA.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IRepository<UserProfile> userProfileRepository;

        public UserProfileService(IRepository<UserProfile> userProfile)
        {
            this.userProfileRepository = userProfile;
        }

        public UserProfile GetUserProfile(long id)
        {
            return userProfileRepository.Get(id);
            throw new NotImplementedException();
        }
    }
}
