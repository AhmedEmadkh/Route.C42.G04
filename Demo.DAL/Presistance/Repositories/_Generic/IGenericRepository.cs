using Demo.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Demo.DAL.Presistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool withAsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable(bool withAsNoTracking = true);
        T? Get(int id);
        void Add(T T);
        void Update(T T);
        void Delete(T T);
    }
}
