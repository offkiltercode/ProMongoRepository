using NUnit.Framework;
using ProMongoRepositoryTests.TestModel;
using ProMongoRepositoryTests.TestRepository;
using SharpTestsEx;

namespace ProMongoRepositoryTests
{
    [TestFixture]
    public class Inheritance_Tests
    {
        
        [Test]
        public void Repository_Should_Have_Correct_Collection_Type()
        {
            var repository = new UserRepository();
            repository.CollectionName.Should().Be("User");

        }

        [Test]
        public void Base_Inheritance_Save()
        {
            var repository = new UserRepository();
            var user = new User {Name = "Base_Inheritance_Save"};
            repository.Save(user);
            repository.CollectionName.Should().Be("User");
        }
    }
}