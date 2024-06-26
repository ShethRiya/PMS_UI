using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.Project;

namespace ProjectManagement_UI.Services.ProjectServices
{
    public class ProjectService: BaseService , IProjectService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public ProjectService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIProject");

        }

        public Task<T> CreateAsync<T>(AddEditProjectViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/project",
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

        public Task<T> GetAllAsync<T>(ProjectFilter paginationDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/project/search",
                Data = paginationDTO,
                //Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/project/" + id,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(AddEditProjectViewModel dto, int Id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/project/" + Id,
                Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool IsActive , string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/project/" + id + "/" + IsActive,
                Token = token
            });

        }

    }
}
