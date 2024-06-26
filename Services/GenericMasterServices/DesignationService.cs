using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.Designation;

namespace ProjectManagement_UI.Services.GenericMasterServices
{
    public class DesignationService : BaseService, IDesignationService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public DesignationService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIEmployee");

        }

        public Task<T> CreateAsync<T>(AddEditDesignationViewModel dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/designation",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/designation/" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(PaginationDTO paginationDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/designation/search",
                Data = paginationDTO

            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/designation/by-id/" + id,
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(EditDesignation dto, int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/designation/update?id=" + id,
                //Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool status)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/designation/update-status?id="+id +"&status="+ status ,
                //Token = token
            });

        }
    }
}
