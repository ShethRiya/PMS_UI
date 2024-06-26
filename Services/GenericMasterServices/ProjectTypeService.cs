using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.ProjectType;

namespace ProjectManagement_UI.Services.GenericMasterServices
{
    public class ProjectTypeService : BaseService , IProjectTypeService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public ProjectTypeService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIProject");

        }

        public Task<T> CreateAsync<T>(AddEditPTYViewModel dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/project-types",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/project-types/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(PaginationDTO paginationDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/project-types/get-all",
                Data = paginationDTO
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/project-types/" + id,
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(AddEditPTYViewModel dto, int statusId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/project-types/" + statusId,
                //Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool status)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/project-types/" + id + "/" + status,
                //Token = token
            });

        }
    }
}
