using Norm;

namespace ProMongoRepository
{
    public class CollectionConnectionContainer
    {
        public string Collection { get; set; }
        public ConnectionStringBuilder ConnectionStringBuilder { get; set; }
    }
}