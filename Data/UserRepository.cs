using DOTNETAPI.Models;

namespace DOTNETAPI.Data
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _entityFrameWork;

        public UserRepository(IConfiguration config)
        {
            _entityFrameWork = new DataContextEF(config);
        }

        public bool SaveChanges()
        {
            return _entityFrameWork.SaveChanges() > 0;
        }

        public void AddEntity<T>(T entityToAdd)
        {
            if(entityToAdd != null)
            {
            _entityFrameWork.Add(entityToAdd);
            }
        }

        public void RemoveEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFrameWork.Remove(entityToAdd);
            }
        }
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFrameWork.Users.ToList<User>();
            return users;
        }

        public IEnumerable<UserSalary> GetAllUsersSalary()
        {
            IEnumerable<UserSalary> usersalary = _entityFrameWork.UserSalary.ToList<UserSalary>();
            return usersalary;

        }

        public IEnumerable<UserJobInfo> GetAllUsersJobInfo()
        {
            IEnumerable<UserJobInfo> usersalljobinfo = _entityFrameWork.UserJobInfo.ToList<UserJobInfo>();
            return usersalljobinfo;

        }

        public User GetSingleUser(int userId)
        {

            User? user = _entityFrameWork.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

            if (user != null)
            {
                return user;
            }

            throw new Exception("Fail to get User");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {

            UserSalary? usersalary = _entityFrameWork.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

            if (usersalary != null)
            {
                return usersalary;
            }

            throw new Exception("Fail to get User");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {

            UserJobInfo? userjobinfo = _entityFrameWork.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

            if (userjobinfo != null)
            {
                return userjobinfo;
            }

            throw new Exception("Fail to get User");
        }


    }

}