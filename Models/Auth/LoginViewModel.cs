namespace ProjectManagement_UI.Models.Auth
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class SessionUser
    {
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //public int accounttype { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string NewPassword { get; set; }
        public string? CreatedBys { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
