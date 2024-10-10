using Demo.BLL.Models.Departments;
using Demo.DAL.Entities.Departments;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.UnitOfWork;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
            }).AsNoTracking().ToListAsync();
            return departments;
        }

        public async Task<DeparmentDetailsDto?> GetDartmentByIdAsync(int Id)
        {
            var departments = await _unitOfWork.DepartmentRepository.GetAsync(Id);

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

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
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
             _unitOfWork.DepartmentRepository.Add(createdDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdatedDepartmentAsync(UpdatedDepartmentDto departmentDto)
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
            _unitOfWork.DepartmentRepository.Update(updatedDepartment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int Id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var department = await departmentRepo.GetAsync(Id);
            if(department is not null)
                departmentRepo.Delete(department);
            
            return await _unitOfWork.CompleteAsync() > 0;
        }
    }
}
