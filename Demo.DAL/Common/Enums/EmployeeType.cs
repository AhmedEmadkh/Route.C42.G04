using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Common.Enums
{
    public enum EmployeeType
    {
        [Display(Name = "Part Time Employee")]
        PartTimeEmployee = 1,

        [Display(Name = "Full Time Employee")]
        FullTimeEmployee = 2,

        [Display(Name = "Internship")]
        Internship = 3,
    }
}
