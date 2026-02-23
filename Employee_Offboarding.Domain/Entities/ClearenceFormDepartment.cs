using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Offboarding.Domain.Entities
{
    public class ClearenceFormDepartment
    {
        public int ClearenceFormId { get; set; }
        public ClearenceForm ClearenceForm { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
    }
}
