using Demo.DAL.Entities.Employees;
using Demo.DAL.Presistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repositories.Employees
{
    internal interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
