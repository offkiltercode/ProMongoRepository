using NUnit.Framework;
using ProMongoRepository;
using ProMongoRepositoryTests.TestModel;
using SharpTestsEx;

namespace ProMongoRepositoryTests
{
    [TestFixture]
    public class Examples
    {
        //You will want to use IoC to create your repo in app code.
        
        [Test]
        public void Using_ConnectionString_From_App_or_Web_Config()
        {
            //The first connectionString that starts with mongodb will be used.
            var mongoRepository = new MongoRepository<User>();
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://localhost/testdb?strict=true");
        }

        [Test]
        public void Using_Named_ConnectionString_From_App_or_Web_Config()
        {
            var mongoRepository = new MongoRepository<User>("MongoDBConnectionString");
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://localhost/testdb?strict=true");
        }

        [Test]
        public void Passing_ConnectionString()
        {
            var mongoRepository = new MongoRepository<User>("mongodb://localhost/testdb?strict=true");
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://localhost/testdb?strict=true");
        }


        [Test]
        public void Creating_Repository_For_User_Collection()
        {
            //User collection will be created if not exist.
            var mongoRepository = new MongoRepository<User>();
            mongoRepository._collection.Name.Should().Be("User");
        }

        [Test]
        public void Overriding_Collection_Name()
        {
            var mongoRepository = new MongoRepository<User>(collection: "users");
            mongoRepository._collection.Name.Should().Be("users");
        }

        [Test]
        public void Basic_Save()
        {
            var mongoRepository = new MongoRepository<User>();
            var user = new User() {Name = "Basic_Save"};
            mongoRepository.Add(user);
        }
    }
}