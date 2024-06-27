namespace ProjectManagement_UI.Models.Project.TaskLog
{
    public class TaskLogFilter
    {
        public string? EmployeeId { get; set; }
        public int? TaskId { get; set; }

        public DateOnly? Date { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string? SortBy { get; set; }

        public bool IsAscending { get; set; }
    }
}
