namespace ProjectManagement_UI.Models.Project
{
    public class ProjectFilter
    {
        public string? Name { get; set; }=null;

        public int? Priority { get; set; } = 1;

        public int? Status { get; set; } = 1;

        public int? TechnoogyId { get; set; } = 1;

        public DateTime? StartDate { get; set; } = DateTime.MinValue;

        public DateTime? EndDate { get; set; } = DateTime.MinValue;

        public int PageSize { get; set; } = 10;

        public int PageNumber { get; set; } = 1;

        public string? SortBy { get; set; } = "CreatedDate";

        public bool IsAscending { get; set; }=false;
    }
    public enum Priority
    {
        High = 1, Low = 2, Medium = 3
    }
}
