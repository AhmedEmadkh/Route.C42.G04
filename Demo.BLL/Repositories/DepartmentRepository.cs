using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    internal class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(Department department)
        {
            _context.Add(department);
            return _context.SaveChanges();
        }

        public int Delete(Department department)
        {
            _context.Remove(department);
            return _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
           return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            //var Department =_context.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            //if(Department is null)
            //{
            //    Department = _context.Departments.Where(D => D.Id == id).FirstOrDefault();
            //}
            //return Department;

            return _context.Departments.Find(id);
        }

        public int Update(Department department)
        {
            _context.Update(department);
            return _context.SaveChanges();
        }
    }
}
