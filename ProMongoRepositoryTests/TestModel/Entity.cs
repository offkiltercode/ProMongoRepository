using Norm;

namespace ProMongoRepositoryTests.TestModel
{
    public class Entity
    {
        [MongoIdentifier]
        public ObjectId Id { get; set; } 
    }
}