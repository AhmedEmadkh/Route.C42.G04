using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
