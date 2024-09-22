using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models.Departments
{
    public class Department : ModelBase
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
