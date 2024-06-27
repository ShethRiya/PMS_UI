using ProjectManagement_UI.Models.Auth;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.ProjectStatus;

namespace ProjectManagement_UI.Services.AuthServices
{
    public class EmployeeService : BaseService , IEmployeeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public EmployeeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIEmployee");

        }
        public Task<T> CreateAsync<T>(CreateUpdateUserRequestDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/employee",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/project-statuses/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(EmployeeFilter paginationDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/employee/search",
                Data = paginationDTO,
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/employee/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CreateUpdateUserRequestDTO dto, int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/employee/" + id,
                Token = token
            });
        }
        public Task<T> StatusChange<T>(Guid id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/employee/" + id ,
                Token = token
            });

        }
    }
}
