namespace ProjectManagement_UI.Models.Project.Task
{
    public class TaskFilter
    {
        public string Description { get; set; }

        public int Priority { get; set; } = 0;

        public int Status { get; set; }

        public int TaskCategoryId { get; set; }

        public DateTime StartDate { get; set; } = default;

        public DateTime EndDate { get; set; }=default;

        public int PageSize { get; set; } = 100;

        public int PageNumber { get; set; } = 1;

        public string? SortBy { get; set; }

        public bool IsAscending { get; set; }
        public int ProjectId { get; set; } 
    }
}
