namespace ProjectManagement_UI.Models.Project
{
    public class ProjectFilter
    {
        public string? Name { get; set; } = null;

        public int? Priority { get; set; } = 0;

        public int? Status { get; set; } = 0;

        public int? TechnologyId { get; set; } = 0;

        public DateTime StartDate { get; set; } = default;

        public DateTime EndDate { get; set; } = default;

        public int PageSize { get; set; } = 100;

        public int PageNumber { get; set; } = 1;

        public string? SortBy { get; set; }

        public bool IsAscending { get; set; }
    }
    public enum Priority
    {
        High = 1, Low = 2, Medium = 3
    }
}
