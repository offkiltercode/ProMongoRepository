using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Norm;

namespace ProMongoRepository
{
    public class MongoRepository<T> : IMongoRepository<T> where T : class, new()
    {
        public MongoRepository(string connectionStringName = "", string collection = "")
        {
            ConnectionString = ConfigurationParser.LoadConfiguration(connectionStringName);
            BuildCollection(collection);
        }

        private void BuildCollection(string collection)
        {
            if (!string.IsNullOrEmpty(collection))
            {
                Collection = collection;
            } 
            else
            {
                Collection = (IsSubClassOfMongoRepository()) ? GetBaseTypeGenericArgument(GetType()).Name : GetType().GetGenericArguments()[0].Name;
            }
        }

        public static Type GetBaseTypeGenericArgument(Type type)
        {
            return (type.BaseType != null) ? type.BaseType.GetGenericArguments()[0] : null ;
        }

        private bool IsSubClassOfMongoRepository()
        {
            var rawGeneric = typeof (MongoRepository<>);
            var subclass = GetType();
            while (subclass != typeof(object))
            {
                var cur = subclass.IsGenericType ? subclass.GetGenericTypeDefinition() : subclass;
                if (rawGeneric == cur)
                {
                    return true;
                }
                subclass = subclass.BaseType;
            }
            return false;
        }

        public ObjectId Id { get; set; }

        public string Collection { get; set; }

        public ConnectionStringBuilder ConnectionString { get; set; }

        public virtual void Save(T instance)
        {

            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).Save(instance);
            }
        }

        public virtual void Save()
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).Save(this as T);
            }
        }

        public void Update(object spec, object newValues)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).UpdateOne(spec, newValues);
            }
        }

        public void UpdateAll(object spec, object newValues, bool upsert = false)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).Update(spec, newValues, true, upsert);
            }
        }

        public virtual T FindById(ObjectId id)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return mongo.GetCollection<T>(Collection).FindOne(new { _id = id });
            }
        }

        public virtual T FindById(string id)
        {
            return FindById(new ObjectId(id));
        }

        public virtual T FindOne(object spec)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return mongo.GetCollection<T>(Collection).FindOne(spec);
            }
        }

        public virtual T FindOne(Func<T, bool> func)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return Enumerable.FirstOrDefault<T>(mongo.GetCollection<T>(Collection).AsQueryable().Where(func));
            }
        }

        public virtual bool Contains(object spec)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                var found = mongo.GetCollection<T>(Collection).Find(spec);
                foreach (var item in found)
                {

                }
                return (found.Count() > 0);
            }
        }

        public virtual IEnumerable<object> Distinct<TY>(string property)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return mongo.GetCollection<TY>(Collection).Distinct<object>(property);
            }
        }

        public virtual IEnumerable<T> Find(object spec)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return mongo.GetCollection<T>(Collection).Find(spec);
            }
        }

        public virtual IEnumerable<T> Find(Func<T, bool> func)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                return Enumerable.Where(mongo.GetCollection<T>(Collection).AsQueryable(), func);
            }
        }

        public virtual IQueryable<T> Linq()
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                var result = mongo.GetCollection<T>(Collection).AsQueryable();
                return result;
            }
        }

        public virtual void RemoveAll()
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).Delete(new { });
            }
        }

        public virtual void Remove(object spec)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                var item = Find(spec);
                mongo.GetCollection<T>(Collection).Delete(item);
            }
        }

        public virtual void Remove(string id)
        {
            Remove(new ObjectId(id));
        }

        public virtual void Remove(ObjectId id)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                mongo.GetCollection<T>(Collection).Delete(new { id = id });
            }
        }

        public IEnumerable<TYMappedType> MapReduce<TYMappedType>(string map, string reduce, string outputCollectioName)
        {
            using (IMongo mongo = Mongo.Create(ConnectionString.ToString()))
            {
                MapReduce mapReduce = mongo.Database.CreateMapReduce();
                mapReduce.Execute(new MapReduceOptions(Collection)
                                      {
                                          Map = map,
                                          Reduce = reduce,
                                          Permanant = false,
                                          OutputCollectionName = outputCollectioName
                                      });

                IQueryable<TYMappedType> collection =
                    mongo.Database.GetCollection<TYMappedType>(outputCollectioName).AsQueryable();

                return collection;
            }
        }
    }
}