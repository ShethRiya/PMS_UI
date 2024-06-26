using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectType
{
    public class ProjectTypeDashboardViewModel : PaginationDTO
    {

        public IEnumerable<PTYDrows> PTYDrows { get; set; }


    }
    public class PTYDrows
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int ProjectCategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
