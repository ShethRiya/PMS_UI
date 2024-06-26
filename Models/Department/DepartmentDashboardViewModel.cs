using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.Department
{
    public class DepartmentDashboardViewModel : PaginationDTO
    {

        public IEnumerable<DepartmentRow> DPRows { get; set; }


    }
    public class DepartmentRow :PaginationDTO
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public string? IsActive { get; set; } = "true";
    }
    public class DepartmentFilterRequestDTO : PaginationDTO
    {
        public string? IsActive { get; set; } = null;
        public string? Name { get; set; } = null;
        public bool OrderBy { get; set; } = true;
    }
}
