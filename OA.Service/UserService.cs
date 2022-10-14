using OA.Dataa;
using OA.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service
{
    // Provide Service 
    public class UserService : IUserService
    {
        // Call Repository<>
        IRepository<User> userRepository;
        IRepository<UserProfile> userProfileRepository;

        public UserService(IRepository<User> userRepository, IRepository<UserProfile> userProfileRepository)
        {
            this.userRepository = userRepository;
            this.userProfileRepository = userProfileRepository;
        }

        public void DeleteUser(long id)
        {
            // Remove user in UserProfile
            UserProfile userProfile = userProfileRepository.Get(id);
            userProfileRepository.Remove(userProfile);
            userProfileRepository.SaveChanges();
            
            // Remove user in User
            User user = userRepository.Get(id);
            userRepository.Remove(user);
            userRepository.SaveChanges();

        }

        public User GetUser(long id)
        {
            return userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetAll();
        }

        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }
    }
}
