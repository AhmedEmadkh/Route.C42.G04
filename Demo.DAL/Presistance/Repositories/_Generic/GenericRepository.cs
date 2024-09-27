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
                return _context.Set<T>().AsNoTracking().ToList();
            return _context.Set<T>().ToList();
        }

        public IQueryable<T> GetAllAsIQueryable(bool withAsNoTracking = true)
        {
            return _context.Set<T>();
        }

        public int Add(T T)
        {
            _context.Set<T>().Add(T);
            return _context.SaveChanges();
        }
        public int Update(T T)
        {
            _context.Set<T>().Update(T);
            return _context.SaveChanges();
        }
        public int Delete(T T)
        {
            _context.Set<T>().Remove(T);
            return _context.SaveChanges();
        }
    }
}
