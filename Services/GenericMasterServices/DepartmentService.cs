using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Models.Department;

namespace ProjectManagement_UI.Services.GenericMasterServices
{
    public class DepartmentService : BaseService , IDepartmentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string pmsUrl;

        public DepartmentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            pmsUrl = configuration.GetValue<string>("ServiceUrls:PMSAPIEmployee");

        }

        public Task<T> CreateAsync<T>(AddEditDepartmentViewModel dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = pmsUrl + "/api/department",
                //Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = pmsUrl + "/api/department?id=" + id,
                //Token = token
            });
        }

        public Task<T> GetAllAsync<T>(DepartmentFilterRequestDTO model)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Url = pmsUrl + "/api/department/get-all",
                Data = model
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = pmsUrl + "/api/department/" + id,
                //Token = token
            });
        }

        public Task<T> UpdateAsync<T>(EditDepartment dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = pmsUrl + "/api/department/" + dto.DepartmentId,
                //Token = token
            });
        }
        public Task<T> StatusChange<T>(int id, bool status)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                //Data = dto,
                Url = pmsUrl + "/api/department/status/" + id + "?IsActive=" + status ,
                //Token = token
            });

        }
    }
}
