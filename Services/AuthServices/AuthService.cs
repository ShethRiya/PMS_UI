using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Auth;
using ProjectManagement_UI.Services.IServices;

namespace ProjectManagement_UI.Services.AuthServices
{
    public class AuthService : BaseService , IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIAuth");

        }

        public Task<T> LoginAsync<T>(LoginViewModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                //Data = obj,
                Url = pmsUrl + "/api/authentication/login?email=" + obj.Email+"&password="+obj.Password,
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationViewModel obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = pmsUrl + "/api/authentication/register"
            });
        }
    }
}
