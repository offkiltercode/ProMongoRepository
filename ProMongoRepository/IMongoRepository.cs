using System;
using System.Linq;
using MongoDB.Bson;

namespace ProMongoRepository
{
    public interface IMongoRepository<T> where T : class
    {
        IQueryable<T> Linq();
        IQueryable<T> GetMany(Func<T, bool> func);

        void Add(T instance);
        void Update(T instance);
      
        T Get(ObjectId id);
        T Get(Func<T, bool> func);
        T Get(string id);
        
        void RemoveAll();
        void Remove(string id);
        void Remove(ObjectId id);
    }
}