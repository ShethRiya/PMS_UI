namespace ProjectManagement_UI.Models.Project.Task
{
    public class CreateUpdateTask
    {
        public int ProjectId { get; set; }

        public int TaskCategoryId { get; set; }

        public string? TaskName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public short Priority { get; set; }

        public string? Description { get; set; }

        public short Status { get; set; }

        public string AssignBy { get; set; } = null!;
    }
}
