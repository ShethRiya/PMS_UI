using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.Auth;

namespace ProjectManagement_UI.Services.AuthServices
{
    public class EmployeeTaskService : BaseService , IEmployeeTaskService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public EmployeeTaskService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIProject");

        }

        public Task<T> CreateAsync<T>(EmployeeTaskCreateUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/task-employee",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/task-employee/" + id,
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(EmployeeTaskFilter paginationDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/task-employee/search",
                Data = paginationDTO,
                Token = token
            });
        }

        

        
    }
}
