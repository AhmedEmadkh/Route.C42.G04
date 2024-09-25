using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withAsNoTracking = true);
        IQueryable<Department> GetAllAsIQueryable(bool withAsNoTracking = true);
        Department? Get(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}
