using AutoMapper;
using Demo.BLL.Models.Departments;
using Demo.BLL.Models.Employees;
using Demo.DAL.Entities.Employees;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Departments;
using Demo.PL.ViewModels.Employees;
using Demo.PL.ViewModels.Users;

namespace Demo.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee
            CreateMap<EmployeeDetailsDto, EmployeeEditCreateViewModel>();
            CreateMap<EmployeeEditCreateViewModel, CreatedEmployeeDto>();
            #endregion

            #region Department

            CreateMap<DepartmentDto, DepartmentEditViewModel>();

            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentEditViewModel, CreatedDepartmentDto>();

            #endregion

            #region User

            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();

            #endregion
        }
    }
}
