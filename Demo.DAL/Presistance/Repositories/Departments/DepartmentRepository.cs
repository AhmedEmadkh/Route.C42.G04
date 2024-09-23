using Demo.DAL.Models.Departments;
using Demo.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repositories.Departments
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if(withAsNoTracking)
                return _context.Departments.AsNoTracking().ToList();
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments.Find(id);
        }
        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }
        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }
    }
}
