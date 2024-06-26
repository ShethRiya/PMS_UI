namespace ProjectManagement_UI.Models.Auth
{
    public class EmployeeFilter
    {
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public int? DesignationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}
