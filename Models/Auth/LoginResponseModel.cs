namespace ProjectManagement_UI.Models.Auth
{
    public class LoginResponseModel
    {

        public UserDTO User { get; set; }
        public string Token { get; set; }

    }
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

    }
}
