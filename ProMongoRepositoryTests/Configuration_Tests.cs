using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProMongoRepository;
using ProMongoRepositoryTests.TestModel;
using SharpTestsEx;

namespace ProMongoRepositoryTests
{
    [TestFixture]
    public class Configuration_Tests
    {
        
        [Test]
        public void Should_Default_To_Web_Config()
        {
            var mongoRepository = new MongoRepository<User>();
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://localhost/testdb?strict=true");
            //mongoRepository.ConnectionString.Server.Should().Be("localhost");
        }

        [Test]
        public void Should_Load_By_Name()
        {
            var mongoRepository = new MongoRepository<User>("MongoDBConnectionString");
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://localhost/testdb?strict=true");
        }

        [Test]
        public void Should_Load_By_Name_2()
        {
            var mongoRepository = new MongoRepository<User>("UsernameAndPassword");
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://myUser:myPass@flame.mongohq.com:27055/DetroitStealthStartup");
        }


        [Test]
        public void Should_Accept_Passed_In_ConnectionString()
        {
            var mongoRepository = new MongoRepository<User>("mongodb://passed:to@flame.mongohq.com:27055/repository");
            mongoRepository._connectionStringBuilder.ToString().Should().Be("mongodb://passed:to@flame.mongohq.com:27055/repository");
        }
        

    }
}
