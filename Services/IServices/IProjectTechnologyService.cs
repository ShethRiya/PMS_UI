using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectTechnology;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectTechnologyservice
    {
        Task<T> GetAllAsync<T>(PaginationDTO paginationDTO, string token);
        Task<T> CreateAsync<T>(AddEditPTViewModel dto, string token);
        Task<T> GetAsync<T>(int id, string token);
        //Task<T> UpdateAsync<T>(AddEditPTViewModel dto, int StatusId);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> StatusChange<T>(int id, bool status, string token);
        Task<T> UpdateAsync<T>(AddEditPTViewModel dto, int statusId, string token);


    }
}
