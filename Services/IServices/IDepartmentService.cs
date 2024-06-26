using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Department;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IDepartmentService
    {
        Task<T> GetAllAsync<T>(DepartmentFilterRequestDTO paginationDTO);
        Task<T> CreateAsync<T>(AddEditDepartmentViewModel dto);
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(EditDepartment dto);
        Task<T> DeleteAsync<T>(int id);
        Task<T> StatusChange<T>(int id, bool status);
    }
}
