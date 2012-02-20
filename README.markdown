## ProMongoRepository

First; there is nothing "pro" about it. It's simply my (@detroitpro) base Mongo Repository class. 

ProMongoRepository sits on top of the offical C# driver and FluentMongo to provide a simple repository pattern.

## Usage

        [Test]
        public void Basic_Save()
        {
            var mongoRepository = new MongoRepository<User>();
            var user = new User() {Name = "Basic_Save"};
            mongoRepository.Save(user);
        }

More [Examples](https://github.com/detroitpro/ProMongoRepository/blob/master/ProMongoRepositoryTests/Examples.cs)