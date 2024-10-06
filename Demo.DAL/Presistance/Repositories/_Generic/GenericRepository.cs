using Demo.DAL.Entities;
using Demo.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo.DAL.Presistance.Repositories._Generic
{
    public class GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _context.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToList();
            return _context.Set<T>().Where(X => !X.IsDeleted).ToList();
        }

        public IQueryable<T> GetAllAsIQueryable(bool withAsNoTracking = true)
        {
            return _context.Set<T>().Where(X => !X.IsDeleted);
        }

        public void Add(T T) => _context.Set<T>().Add(T);
        
        public void Update(T T) => _context.Set<T>().Update(T);
        
        public void Delete(T T)
        {
            T.IsDeleted = true;
            _context.Set<T>().Update(T);
        }
    }
}
