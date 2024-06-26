using ProjectManagement_UI.Models.Auth;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IAuthService
    {
        Task<T> RegisterAsync<T>(RegistrationViewModel obj);
        Task<T> LoginAsync<T>(LoginViewModel obj);


    }
}
