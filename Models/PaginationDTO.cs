namespace ProjectManagement_UI.Models
{
    public class PaginationDTO
    {
        public string Name { get; set; } = "";
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public string SortBy { get; set; } = "CreatedDate";
        public bool IsAscending { get; set; } = false;

        public int CurrentPage { get; set; } = 1;
    }
}
