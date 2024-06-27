using ProjectManagement_UI.Models.ProjectType;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Project.Task;

namespace ProjectManagement_UI.Services.IServices
{
    public interface ITaskService
    {
        Task<T> GetAllAsync<T>(TaskFilter paginationDTO, string token);
        Task<T> CreateAsync<T>(CreateUpdateTask dto, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(int Id, CreateUpdateTask dto,  string token);
        //Task<T> DeleteAsync<T>(int id, string token);
        Task<T> StatusChange<T>(int id, bool status, string token);
    }
}
