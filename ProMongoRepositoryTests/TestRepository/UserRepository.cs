using System.Collections.Generic;
using System.Linq;
using ProMongoRepository;
using ProMongoRepositoryTests.TestModel;

namespace ProMongoRepositoryTests.TestRepository
{
    public class UserRepository : MongoRepository<User>
    {
        public List<User> GetAllUsersFromRepository()
        {
            return this.Linq().ToList();
        }
    }
}