using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectTechnology
{
    public class AddEditPTViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
