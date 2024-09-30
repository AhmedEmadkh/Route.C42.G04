using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
    }
}
