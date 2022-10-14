using OA.Dataa;

namespace OA.Service
{
    public interface IUserService
    {
        // Provide method get User - Insert - Update
        IEnumerable<User> GetUsers();
        User GetUser(long id);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long id);
    }
}