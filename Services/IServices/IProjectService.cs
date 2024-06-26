﻿using ProjectManagement_UI.Models.ProjectType;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Project;

namespace ProjectManagement_UI.Services.IServices
{
    public interface IProjectService
    {
        Task<T> GetAllAsync<T>(ProjectFilter paginationDTO );
        Task<T> CreateAsync<T>(AddEditProjectViewModel dto, string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<T> UpdateAsync<T>(AddEditProjectViewModel dto, int Id, string token);
        //Task<T> DeleteAsync<T>(int id);
        Task<T> StatusChange<T>(int id, bool IsActive, string token);
    }
}
