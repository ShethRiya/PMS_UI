using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectType
{
    public class AddEditPTYViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
