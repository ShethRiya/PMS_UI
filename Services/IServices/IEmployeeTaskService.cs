using ProjectManagement_UI.Models.Auth;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IEmployeeTaskService
    {
        Task<T> GetAllAsync<T>(EmployeeTaskFilter paginationDTO, string token);
        Task<T> DeleteAsync<T>(int id, string token);
        Task<T> CreateAsync<T>(EmployeeTaskCreateUpdateDTO dto, string token);



    }
}
