using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectStatus;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectStatusService
    {
        Task<T> GetAllAsync<T>(PaginationDTO paginationDTO);
        Task<T> CreateAsync<T>(AddEditPSViewModel dto);
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(AddEditPSViewModel dto , int statusId);
        Task<T> DeleteAsync<T>(int id);
        Task<T> StatusChange<T>(int id , bool status);
    }
}
