using System.ComponentModel.DataAnnotations;

namespace Employee_Offboarding.Domain.Entities
{
    public enum WorkplaceType
    {
        [Display(Name = "სათაო ოფისი")]
        HeadquartersDepartment = 1,
        [Display(Name = "სერვის ცენტრი")]
        ServiceCenter = 2
    }
    public class Workplace
    {
        public int Id { get; set; }
        public WorkplaceType Type { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
