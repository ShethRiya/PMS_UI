using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models.ProjectTechnology;
using ProjectManagement_UI.Models.ProjectType;

namespace ProjectManagement_UI.Models.Project
{
    public class ProjectViewModel : ProjectFilter
    {
        public List<PTDrow>? TechnologyRows { get; set; }
        //public List<DesignationRow> DesignationRows{ get; set; }
        public List<PSDrow>? PSDrows { get; set; }
        public List<ProjectRow>? ProjectRows { get; set; }
        public int? ProjectId { get; set; }
        public List<PTYDrows>? PTYDrows { get; set; }
        public int? ProjectCategoryId { get; set; }
        public ProjectRow ProjectRow { get; set; }
        public CreateProjectViewModel CreateProjectViewModel { get; set; }
        //for editing
        public string? Description { get; set; }

    }
    public class ProjectRow
    {
        public int ProjectId { get; set; }

        public string? Description { get; set; }

        public int ProjectCategoryId { get; set; }

        public string? ProjectCategory { get; set; }

        public string ProjectManagerId { get; set; }

        public string? ProjectManagerName { get; set; }

        public int TechnologyId { get; set; }

        public string TechnologyName { get; set; }

        public string? Status { get; set; }

        public string? Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public string Name { get; set; } = null!;
    }
}
