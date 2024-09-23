using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models.Departments
{
    internal class DeparmentDetailsToReturnDto
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
