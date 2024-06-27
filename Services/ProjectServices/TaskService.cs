using ProjectManagement_UI.Models.Project;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.Project.Task;

namespace ProjectManagement_UI.Services.ProjectServices
{
    public class TaskService : BaseService, ITaskService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public TaskService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIProject");

        }

        public Task<T> CreateAsync<T>(CreateUpdateTask dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/task",
                Token = token
            });
        }

        //public Task<T> DeleteAsync<T>(int id)
        //{
        //    return SendAsync<T>(new APIRequest()
        //    {
        //        ApiType = SD.ApiType.DELETE,
        //        Url = pmsUrl + "/api/designation/" + id,
        //        //Token = token
        //    });
        //}

        public Task<T> GetAllAsync<T>(TaskFilter paginationDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/task/search",
                Data = paginationDTO,
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/task/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(int Id, CreateUpdateTask dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/task/" + Id,
                Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool IsActive, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/task/" + id + "/" + IsActive,
                Token = token
            });

        }
    }
}
