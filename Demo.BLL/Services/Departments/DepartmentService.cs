using Demo.BLL.Models.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToList();
            return departments;
        }

        public DeparmentDetailsDto GetDartmentById(int Id)
        {
            var departments = _departmentRepository.Get(Id);

            if (departments is not null)
                return new DeparmentDetailsDto
                {
                    Id = departments.Id,
                    Code = departments.Code,
                    Name = departments.Name,
                    Description = departments.Description,
                    CreationDate = departments.CreationDate,
                    CreatedBy = departments.CreatedBy,
                    CreatedOn = departments.CreatedOn,
                    LastModifiedBy = departments.LastModifiedBy,
                    LastModifiedOn = departments.LastModifiedOn,
                };
            return null;
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var createdDepartment = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy= 1,
                LastModifiedOn = DateTime.UtcNow
            };
            return _departmentRepository.Add(createdDepartment);
        }

        public int UpdatedDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy= 1,
                LastModifiedOn = DateTime.UtcNow
            };
            return _departmentRepository.Update(updatedDepartment);
        }

        public bool DeleteDepartment(int Id)
        {
            var department = _departmentRepository.Get(Id);
            if(department is not null){
                return _departmentRepository.Delete(department) > 0;
            }
            return false;
        }
    }
}
