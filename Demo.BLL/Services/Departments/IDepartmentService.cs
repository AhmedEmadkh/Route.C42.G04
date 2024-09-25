using Demo.BLL.Models.Departments;
using Demo.DAL.Entities.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments();
        DeparmentDetailsDto? GetDartmentById(int Id);
        int CreateDepartment(CreatedDepartmentDto departmentDto);
        int UpdatedDepartment(UpdatedDepartmentDto departmentDto);
        bool DeleteDepartment(int Id);
    }
}
