using Demo.DAL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repositories.Departments
{
    internal interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withAsNoTracking = true);
        Department? GetById(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}
