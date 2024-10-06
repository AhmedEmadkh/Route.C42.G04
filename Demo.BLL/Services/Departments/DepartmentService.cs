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

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAllAsIQueryable().Select(department => new DepartmentDto
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
            var departments = _unitOfWork.DepartmentRepository.Get(Id);

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
             _unitOfWork.DepartmentRepository.Add(createdDepartment);
            return _unitOfWork.Complete();
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
            _unitOfWork.DepartmentRepository.Update(updatedDepartment);
            return _unitOfWork.Complete();
        }

        public bool DeleteDepartment(int Id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;
            var department = departmentRepo.Get(Id);
            if(department is not null)
                departmentRepo.Delete(department);
            
            return _unitOfWork.Complete() > 0;
        }
    }
}
