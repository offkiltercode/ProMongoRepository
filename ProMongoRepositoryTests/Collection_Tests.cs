using NUnit.Framework;
using ProMongoRepository;
using ProMongoRepositoryTests.TestModel;
using SharpTestsEx;

namespace ProMongoRepositoryTests
{
    [TestFixture]
    public class Collection_Tests
    {

          [Test]
          public void If_Collection_Name_Is_Not_Passed_To_Repository()
          {
              var mongoRepository = new MongoRepository<User>();
              mongoRepository.CollectionName.Should().Be("User");
          }

          [Test]
          public void If_Collection_Name_Is_Passed_To_Repository()
          {
              var mongoRepository = new MongoRepository<User>(collection:"UserCollection");
              mongoRepository.CollectionName.Should().Be("UserCollection");
          }
    }
}