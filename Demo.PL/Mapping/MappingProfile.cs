using AutoMapper;
using Demo.BLL.Models.Departments;
using Demo.BLL.Models.Employees;
using Demo.DAL.Entities.Employees;
using Demo.PL.ViewModels.Departments;

namespace Demo.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee
            CreateMap<CreatedEmployeeDto, Employee>();
            #endregion

            #region Department

            CreateMap<DepartmentDto, DepartmentEditViewModel>();

            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentEditViewModel, CreatedDepartmentDto>();

            #endregion
        }
    }
}
