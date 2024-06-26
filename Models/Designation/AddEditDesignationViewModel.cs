using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.Designation
{
    public class AddEditDesignationViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
    public class EditDesignation
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int DesignationId { get; set; }

    }
}
