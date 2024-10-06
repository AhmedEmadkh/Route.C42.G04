using AutoMapper;
using Demo.BLL.Models.Departments;
using Demo.PL.ViewModels.Departments;

namespace Demo.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Employee

            #endregion

            #region Department

            CreateMap<DepartmentDto, DepartmentEditViewModel>();

            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDto>();

            CreateMap<DepartmentEditViewModel, CreatedDepartmentDto>();

            #endregion
        }
    }
}
