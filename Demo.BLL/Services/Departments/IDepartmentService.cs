using Demo.BLL.Models.Departments;
using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    internal interface IDepartmentService
    {
        IEnumerable<DepartmentToReturnDto> GetAllDepartments();
        DeparmentDetailsToReturnDto? GetDartmentById(int Id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdatedDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int Id);
    }
}
