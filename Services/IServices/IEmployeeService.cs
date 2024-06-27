using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Auth;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IEmployeeService 
    {
        Task<T> GetAllAsync<T>(EmployeeFilter paginationDTO,string token);
        Task<T> CreateAsync<T>(CreateUpdateUserRequestDTO dto, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(CreateUpdateUserRequestDTO dto, int id, string token);
        //Task<T> DeleteAsync<T>(Guid id);
        Task<T> StatusChange<T>(Guid id, string token);
    }
}
