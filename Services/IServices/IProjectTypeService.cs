using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectType;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectTypeService
    {
        Task<T> GetAllAsync<T>(PaginationDTO paginationDTO);
        Task<T> CreateAsync<T>(AddEditPTYViewModel dto);
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(AddEditPTYViewModel dto, int statusId);
        Task<T> DeleteAsync<T>(int id);
        Task<T> StatusChange<T>(int id, bool status);

    }
}
