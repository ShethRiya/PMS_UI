using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectTechnology;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectTechnologyservice
    {
        Task<T> GetAllAsync<T>(PaginationDTO paginationDTO);
        Task<T> CreateAsync<T>(AddEditPTViewModel dto);
        Task<T> GetAsync<T>(int id);
        //Task<T> UpdateAsync<T>(AddEditPTViewModel dto, int StatusId);
        Task<T> DeleteAsync<T>(int id);
        Task<T> StatusChange<T>(int id, bool status);
        Task<T> UpdateAsync<T>(AddEditPTViewModel dto, int statusId);


    }
}
