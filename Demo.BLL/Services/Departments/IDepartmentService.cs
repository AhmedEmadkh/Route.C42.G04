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
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();
        Task<DeparmentDetailsDto?> GetDartmentByIdAsync(int Id);
        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);
        Task<int> UpdatedDepartmentAsync(UpdatedDepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(int Id);
    }
}
