using System;
using System.Linq;
using FluentMongo.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ProMongoRepository
{
    public class MongoRepository<T> : IDisposable, IMongoRepository<T> where T : class, new()
    {
        protected internal MongoServer _server;
        protected internal readonly MongoDatabase _mongoDatabase;
        protected internal MongoCollection<T> _collection;
        protected internal ConnectionStringBuilder _connectionStringBuilder;
        protected internal string _collectionName;


        public MongoRepository(string connectionStringName = "", string collection = "")
        {
            _connectionStringBuilder = ConfigurationParser.LoadConfiguration(connectionStringName);
            _collectionName = DetermineCollectionName(collection);
            _server = MongoServer.Create(_connectionStringBuilder.ToString());
            _mongoDatabase = _server.GetDatabase(_connectionStringBuilder.Database);
            _collection = _mongoDatabase.GetCollection<T>(_collectionName);
        }


        private string DetermineCollectionName(string collection)
        {
            if (!string.IsNullOrEmpty(collection))
            {
                return collection;
            }
            return IsSubClassOfMongoRepository() ? GetBaseTypeGenericArgument(GetType()).Name : GetType().GetGenericArguments()[0].Name;

        }

        private static Type GetBaseTypeGenericArgument(Type type)
        {
            return (type.BaseType != null) ? type.BaseType.GetGenericArguments()[0] : null;
        }

        private bool IsSubClassOfMongoRepository()
        {
            var rawGeneric = typeof(MongoRepository<>);
            var subclass = GetType();
            //Console.WriteLine(subclass.Name);
            if (subclass.Name == "MongoRepository`1")
            {
                return false;
            }
            while (subclass != typeof(object))
            {
                var cur = subclass != null && subclass.IsGenericType ? subclass.GetGenericTypeDefinition() : subclass;
                if (rawGeneric == cur)
                {
                    return true;
                }
                subclass = subclass.BaseType;
            }
            return false;
        }

        //TODO: review the dispose code based on mongo server connection handling.
        public void Dispose()
        {
            _server.Disconnect();
            _server = null;
        }

        public IQueryable<T> Linq()
        {
            return _collection.AsQueryable();
        }

        public IQueryable<T> GetMany(Func<T, bool> func)
        {
            return _collection.AsQueryable().Where(func).AsQueryable();
        }

        public void Add(T instance)
        {
            _collection.Insert(instance);
        }

        public void Update(T instance)
        {
            //_collection.Update()
        }

        public T Get(ObjectId id)
        {
            return _collection.FindOneById(BsonValue.Create(id));
        }

        public T Get(Func<T, bool> func)
        {
            return _collection.AsQueryable().Where(func).FirstOrDefault();
        }

        public T Get(string id)
        {
            return _collection.FindOneById(BsonValue.Create(id));
        }

        public void RemoveAll()
        {
            _collection.RemoveAll();
        }

        public void Remove(string id)
        {
            var query = Query.EQ("_id", id.ToBson());
            _collection.Remove(query);
        }

        public void Remove(ObjectId id)
        {
            throw new NotImplementedException();
        }





























        //public virtual void Save(T instance)
        //{
        //    //mongo.GetCollection<T>(CollectionName).Save(instance);

        //}



        //public void Update(object spec, object newValues)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        .GetCollection<T>(CollectionName).UpdateOne(spec, newValues);
        //    }
        //}


        //public void UpdateAll(object spec, object newValues, bool upsert = false)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        mongo.GetCollection<T>(CollectionName).Update(spec, newValues, true, upsert);
        //    }
        //}

        //public virtual T FindById(ObjectId id)
        //{
        //    return Collection.FindOneById(BsonValue.Create(id));
        //}

        //public virtual T FindById(string id)
        //{
        //    return FindById(new ObjectId(id));
        //}

        //public virtual T FindOne(object spec)
        //{
        //    return Collection.FindOne(new { author = "Kurt Vonnegut" });
        //}

        //public virtual T FindOne(Func<T, bool> func)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        return Enumerable.FirstOrDefault<T>(mongo.GetCollection<T>(CollectionName).AsQueryable().Where(func));
        //    }
        //}

        //public virtual bool Contains(object spec)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        var found = mongo.GetCollection<T>(CollectionName).Find(spec);
        //        foreach (var item in found)
        //        {

        //        }
        //        return (found.Count() > 0);
        //    }
        //}

        //public virtual IEnumerable<object> Distinct<TY>(string property)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        return mongo.GetCollection<TY>(CollectionName).Distinct<object>(property);
        //    }
        //}

        //public virtual IEnumerable<T> Find(object spec)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        return mongo.GetCollection<T>(CollectionName).Find(spec);
        //    }
        //}

        //public virtual IEnumerable<T> Find(Func<T, bool> func)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        return Enumerable.Where(mongo.GetCollection<T>(CollectionName).AsQueryable(), func);
        //    }
        //}

        //public virtual IQueryable<T> Linq()
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        var result = mongo.GetCollection<T>(CollectionName).AsQueryable();
        //        return result;
        //    }
        //}

        //public virtual void RemoveAll()
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        mongo.GetCollection<T>(CollectionName).Delete(new { });
        //    }
        //}

        //public virtual void Remove(object spec)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        var item = Find(spec);
        //        mongo.GetCollection<T>(CollectionName).Delete(item);
        //    }
        //}

        //public virtual void Remove(string id)
        //{
        //    Remove(new ObjectId(id));
        //}

        //public virtual void Remove(ObjectId id)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        mongo.GetCollection<T>(CollectionName).Delete(new { id = id });
        //    }
        //}

        //public IEnumerable<TYMappedType> MapReduce<TYMappedType>(string map, string reduce, string outputCollectioName)
        //{
        //    using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
        //    {
        //        MapReduce mapReduce = mongo.Database.CreateMapReduce();
        //        mapReduce.Execute(new MapReduceOptions(CollectionName)
        //                              {
        //                                  Map = map,
        //                                  Reduce = reduce,
        //                                  Permanant = false,
        //                                  OutputCollectionName = outputCollectioName
        //                              });

        //        IQueryable<TYMappedType> collection =
        //            mongo.Database.GetCollection<TYMappedType>(outputCollectioName).AsQueryable();

        //        return collection;
        //    }
        //}

    }
}