using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Project;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Services.GenericMasterServices;
using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models.ProjectTechnology;

namespace ProjectManagement_UI.Controllers
{
    
    public class ProjectController : Controller
    {
        private readonly IProjectStatusService _projectStatusService;
        private readonly IProjectTechnologyservice _projectTechnologyService;
        private readonly IProjectService _projectService;
        public ProjectController(IProjectStatusService projectStatusService, IProjectTechnologyservice projectTechnologyservice, IProjectService projectService, IDesignationService designationService, IDepartmentService departmentService)
        {
            _projectService = projectService;
            _projectTechnologyService = projectTechnologyservice;
            _projectStatusService = projectStatusService;
           
        }


        public async Task<IActionResult> Project()
        {
            ProjectViewModel model = new ProjectViewModel();

            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageNumber = 1;
            paginationDTO.PageSize = 100;
            var statuses = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO);
            List<PSDrow> list1 = new List<PSDrow>();
            if (statuses != null && statuses.IsSuccess)
            {
                list1 = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(statuses.Result));
            }
            model.PSDrows = list1;

            var technology = await _projectTechnologyService.GetAllAsync<APIResponse>(paginationDTO);
            List<PTDrow> list2 = new List<PTDrow>();
            if (technology != null && technology.IsSuccess)
            {
                list2 = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(technology.Result));
            }
            model.TechnologyRows = list2;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProjectPartialTable(string Name, int Priority, int Status, int Technology,  int PageSize, int PageNumber, string SortBy, bool IsAscending)
        {
            ProjectViewModel model = new ProjectViewModel();
            ProjectFilter filter = new()
            {
                Name = Name,
                Priority = Priority,
                Status = Status,
                TechnoogyId = Technology,
               //StartDate = default,
               //EndDate = default,
                PageSize = 100,
                PageNumber = 1,
                SortBy = SortBy,
                IsAscending = IsAscending
            };
           // string token1 = HttpContext.Session.GetString(SD.SessionToken);
            var result = await _projectService.GetAllAsync<APIResponse>(filter);
            List<ProjectRow> list1 = new List<ProjectRow>();
            if (result != null && result.IsSuccess)
            {
                list1 = JsonConvert.DeserializeObject<List<ProjectRow>>(Convert.ToString(result.Result));
            }
            model.ProjectRows = list1;
            return PartialView("_ProjectPartialTable", model);

        }

        public async Task<IActionResult> EditProject(int Id)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            var ps = await _projectService.GetAsync<APIResponse>(Id, token);

            if (ps == null)
            {
                TempData["failure"] = "Project not found";
                return View("e");
            }
            ProjectRow projectRow = new ProjectRow();
            projectRow = JsonConvert.DeserializeObject<ProjectRow>(Convert.ToString(ps.Result));
            ProjectViewModel model= new ProjectViewModel();

            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageSize = 100;
            
            DepartmentFilterRequestDTO departmentRow = new DepartmentFilterRequestDTO();
            departmentRow.PageSize = 100;
            var department = await _projectTechnologyService.GetAllAsync<APIResponse>(departmentRow);
            List<PTDrow> list2 = new List<PTDrow>();
            if (department != null && department.IsSuccess)
            {
                list2 = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(department.Result));
            }
            model.TechnologyRows = list2;
            var response = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO);
            List<PSDrow> list = new List<PSDrow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(response.Result));
            }
            model.PSDrows = list;
            return PartialView("_EditProject", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(ProjectRow model)
        {

            if (ModelState.IsValid)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);
                AddEditProjectViewModel model1 = new AddEditProjectViewModel();
                model1.Name = model.Name;
                var result = await _projectService.UpdateAsync<APIResponse>(model1, model.ProjectId , token);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully edited project  details";
                    return RedirectToAction("Project");
                }

                TempData["failure"] = "Error occured during  editing project  details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("Project");
        }

    }
}
