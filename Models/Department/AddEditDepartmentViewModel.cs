using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.Department
{
    public class AddEditDepartmentViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

    }
    public class EditDepartment
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int DepartmentId { get; set; }

    }
}
