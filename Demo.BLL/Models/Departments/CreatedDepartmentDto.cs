using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.BLL.Models.Departments
{
    public class CreatedDepartmentDto
    {
        public int Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
