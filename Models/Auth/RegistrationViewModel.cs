using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models.Designation;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.Auth
{
    public class RegistrationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(10)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string NewPassword { get; set; }
        public IFormFile ProfilePhoto { get; set; }
        public List<DesignationRow> designationRows { get; set; }
        public List<DepartmentRow> departmentRows { get; set; }
        public List<RoleRow> roleRows { get; set; }

    }
}
