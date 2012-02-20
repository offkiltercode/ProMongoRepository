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
            repository._collectionName.Should().Be("User");

        }

        [Test]
        public void Base_Inheritance_Save()
        {
            var repository = new UserRepository();
            var user = new User {Name = "Base_Inheritance_Save"};
            repository.Add(user);
            repository._collectionName.Should().Be("User");
        }
    }
}