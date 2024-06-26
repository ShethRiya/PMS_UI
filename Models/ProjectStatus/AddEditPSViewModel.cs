using System.ComponentModel.DataAnnotations;

namespace ProjectManagement_UI.Models.ProjectStatus
{
    public class AddEditPSViewModel
    {
   
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

    }
}
