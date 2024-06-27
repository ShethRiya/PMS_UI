using ProjectManagement_UI.Models.ProjectType;

namespace ProjectManagement_UI.Models.Project.Task
{
    public class TaskViewModel : CreateUpdateTask 
    {
        public List<PTYDrows>? PTYDrows { get; set; }
        public int ProjectId { get; set; }
        public List<TaskRow> TaskRows { get; set; }

    }
    public class TaskRow
    {
        public int TaskId { get; set; }

        public string? ProjectName { get; set; }

        public string? TaskName { get; set; }

        //public int ProjectId { get; set; }

        //public int TaskCategoryId { get; set; }

        public string? TaskCategory { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Priority { get; set; }

        public string? Description { get; set; }

        public string Status { get; set; }

        public string AssignBy { get; set; } = null!;

        public DateTime AssignDate { get; set; }

        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime? CompletedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
