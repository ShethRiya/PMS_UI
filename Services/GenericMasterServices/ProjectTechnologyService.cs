using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models.ProjectTechnology;
using ProjectManagement_UI.Services.IServices;

namespace ProjectManagement_UI.Services.GenericMasterServices
{
    public class ProjectTechnologyService : BaseService , IProjectTechnologyservice
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public ProjectTechnologyService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIProject");

        }
        public Task<T> GetAllAsync<T>(PaginationDTO paginationDTO, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/project-technology/get-all",
                Data = paginationDTO,
                Token = token
            });
        }
        public Task<T> CreateAsync<T>(AddEditPTViewModel dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/project-technology",
                Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool status, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/project-technology/" + id + "/" + status,
                Token = token
            });

        }
        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/project-technology/" + id,
                Token = token
            });
        }
        public Task<T> UpdateAsync<T>(AddEditPTViewModel dto, int statusId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/project-technology/" + statusId,
                Token = token
            });
        }
        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/project-technology/" + id,
                Token = token
            });
        }

    }
}
