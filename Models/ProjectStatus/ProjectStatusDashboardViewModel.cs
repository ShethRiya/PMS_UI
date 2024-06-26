using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectStatus
{
    public class ProjectStatusDashboardViewModel : PaginationDTO
    {

        public IEnumerable<PSDrow> PSDrows { get; set; }


    }
    public class PSDrow
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
