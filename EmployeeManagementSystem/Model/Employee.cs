using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Model
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide a valid name.")]
        [RegularExpression(@"^[a-zA-Z\s]{1,30}$", ErrorMessage = "Full name should contain only alphabetical characters and spaces, with a maximum length of 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a valid age.")]
        [Range(21, 100, ErrorMessage = "Age must be between 18 and 120.")]
        public int Age { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department department { get; set; }

        [Required(ErrorMessage = "Please provide a valid salary.")]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }
    }
}
