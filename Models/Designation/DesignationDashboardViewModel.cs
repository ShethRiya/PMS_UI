using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.Designation
{
    public class DesignationDashboardViewModel : PaginationDTO
    {

        public IEnumerable<DesignationRow> DSRows { get; set; }


    }
    public class DesignationRow
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int DesignationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsActive { get; set; }

        //DesignationFilter
        public bool? isActiveRecord { get; set; }
        public string searchName { get; set; } = null;
        public int pageNumber { get; set; } = 1;
        public int pageSize { get; set; } = 100;
        public string sortBy { get; set; } = null;
        public bool isAscending { get; set; } = true;
    }
}
