using System;
using System.Collections.Generic;
using System.Linq;
using Norm;

namespace ProMongoRepository
{
    public interface IMongoRepository<T> where T : class
    {
        void Save(T instance);
        void Save();
        void Update(object spec, object newValues);
        void UpdateAll(object spec, object newValues, bool upsert = false);
        T FindById(ObjectId id);
        T FindById(string id);
        T FindOne(object spec);
        T FindOne(Func<T, bool> func);
        IEnumerable<object > Distinct<TY>(string property);
        IEnumerable<T> Find(object spec);
        IEnumerable<T> Find(Func<T, bool> func);
        IQueryable<T> Linq();
        void RemoveAll();
        void Remove(object spec);
        void Remove(string id);
        void Remove(ObjectId id);
        IEnumerable<TYMappedType> MapReduce<TYMappedType>(string map, string reduce, string outputCollectioName);
        ConnectionStringBuilder ConnectionString { get; set; }
        string Collection { get; set; }
    }
}