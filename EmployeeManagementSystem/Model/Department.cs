using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeManagementSystem.Model
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [DisplayName("Department Name")]
        [StringLength(50, ErrorMessage = "Department name must be at most 50 characters.")]
        [Required]
        public string Name { get; set; }
    }
}
