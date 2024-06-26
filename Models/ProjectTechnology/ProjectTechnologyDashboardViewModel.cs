using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectTechnology
{
    public class ProjectTechnologyDashboardViewModel : PaginationDTO
    {

        public IEnumerable<PTDrow> PTDrows { get; set; }


    }
    public class PTDrow
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
    
}
