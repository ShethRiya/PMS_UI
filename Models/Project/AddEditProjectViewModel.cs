namespace ProjectManagement_UI.Models.Project
{
    public class AddEditProjectViewModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public int ProjectCategoryId { get; set; }

        public string? ProjectManagerId { get; set; }

        public int TechnologyId { get; set; }

        public short? Status { get; set; }

        public short? Priority { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
