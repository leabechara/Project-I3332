using System;
using System.Collections.Generic;

namespace ScheduleApplication.Models.Data
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T? Get(int id);
        T? Get(QueryOptions<T> options);
        IEnumerable<T> List(QueryOptions<T> options);
        void Save();
    }
}
