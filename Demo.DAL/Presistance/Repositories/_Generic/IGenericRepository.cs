using Demo.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Demo.DAL.Presistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
        IQueryable<T> GetAllAsIQueryable(bool withAsNoTracking = true);
        void Add(T T);
        void Update(T T);
        void Delete(T T);
    }
}
