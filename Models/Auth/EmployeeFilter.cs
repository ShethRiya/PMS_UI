namespace ProjectManagement_UI.Models.Auth
{

    public class AssignTaskViewModel
    {
        public List<UserResponseDTO> Users { get; set; }
        public int TaskId { get; set; }
        public string EmployeeId  { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }

    }
    public class EmployeeTaskCreateUpdateDTO
    {
        public int TaskId { get; set; }

        public List<string> EmployeeId { get; set; } = new List<string>();

        public string ReportTo { get; set; }

    }
    public class EmployeeFilter
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
    public class CreateUpdateUserRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
    }

    
    public class EmployeeTaskFilter
    {
        public int EmployeeId { get; set; }

        public int TaskId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? SortBy { get; set; }

        public bool IsAscending { get; set; }
    }
    public class EmployeeTaskResponseDTO
    {
        public string? TaskName { get; set; }

        public string? EmployeeName { get; set; }
    }
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? Gender { get; set; }
        public string? PermanentAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public int? DesignationId { get; set; }
        public string? Designation { get; set; }
        public int? DepartmentId { get; set; }
        public string? Department { get; set; }
        public int? RoleId { get; set; }
        public string? Role { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? ProfilePhotoPath { get; set; }

    }
}
