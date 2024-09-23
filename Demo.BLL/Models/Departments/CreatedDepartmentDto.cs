using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models.Departments
{
    internal class CreatedDepartmentDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
