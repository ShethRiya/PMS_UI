namespace ProjectManagement_UI.Models.Project.TaskLog
{
    public class CreateUpdateTaskLog
    {
        public int TaskId { get; set; }
        public string EmployeeId { get; set; }
        public DateOnly Date { get; set; }

        public int Hours { get; set; }

        public int Minutes { get; set; }
    }
}
