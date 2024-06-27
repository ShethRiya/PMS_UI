using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.ProjectStatus;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectStatusService
    {
        Task<T> GetAllAsync<T>(PaginationDTO paginationDTO , string token);
        Task<T> CreateAsync<T>(AddEditPSViewModel dto, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(AddEditPSViewModel dto , int statusId, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> StatusChange<T>(int id , bool status, string token);
    }
}
