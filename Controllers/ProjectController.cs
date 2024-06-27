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
using ProjectManagement_UI.Services.AuthServices;
using ProjectManagement_UI.Models.ProjectType;
using ProjectManagement_UI.Models.Project.Task;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using ProjectManagement_UI.Models.Auth;

namespace ProjectManagement_UI.Controllers
{
    [CustomAuthorize(1)]
    public class ProjectController : Controller
    {
        private readonly IProjectStatusService _projectStatusService;
        private readonly IProjectTechnologyservice _projectTechnologyService;
        private readonly IProjectService _projectService;
        private readonly IProjectTypeService _projectTypeService;
        private readonly ITaskService _taskService;
        private readonly IEmployeeTaskService _employeeTaskService;
        private readonly IEmployeeService _employeeService;
        public ProjectController(IProjectStatusService projectStatusService, IProjectTechnologyservice projectTechnologyservice, IProjectService projectService, IDesignationService designationService, IDepartmentService departmentService, IProjectTypeService projectTypeService, ITaskService taskService, IEmployeeTaskService employeeTaskService, IEmployeeService employeeService)
        {
            _taskService = taskService;
            _projectService = projectService;
            _projectTechnologyService = projectTechnologyservice;
            _projectStatusService = projectStatusService;
            _projectTypeService = projectTypeService;
            _employeeTaskService = employeeTaskService;
            _employeeService = employeeService;
        }

        #region Project
        public async Task<IActionResult> Project()
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);

            ProjectViewModel model = new ProjectViewModel();

            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageNumber = 1;
            paginationDTO.PageSize = 100;
            var statuses = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PSDrow> list1 = new List<PSDrow>();
            if (statuses != null && statuses.IsSuccess)
            {
                list1 = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(statuses.Result));
            }
            model.PSDrows = list1;

            var technology = await _projectTechnologyService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTDrow> list2 = new List<PTDrow>();
            if (technology != null && technology.IsSuccess)
            {
                list2 = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(technology.Result));
            }
            model.TechnologyRows = list2;


            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ProjectPartialTable(string Name, int Priority, int Status, int Technology, int PageSize, int PageNumber, string SortBy, bool IsAscending)
        {
            ProjectViewModel model = new ProjectViewModel();
            ProjectFilter filter = new()
            {
                Name = Name,
                Priority = Priority,
                Status = Status,
                TechnologyId = Technology,
                //StartDate = default,
                //EndDate = default,
                //PageSize = 100,
                //PageNumber = 1,
                SortBy = SortBy,
                IsAscending = IsAscending
            };
            if (PageSize > 0 && PageNumber > 0)
            {
                filter.PageNumber = PageNumber;
                filter.PageSize = PageSize;
            }
            string token1 = HttpContext.Session.GetString(SD.SessionToken);
            var result = await _projectService.GetAllAsync<APIResponse>(filter, token1);
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
            ProjectViewModel model = new ProjectViewModel();
            model.ProjectRow = projectRow;
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageSize = 100;

            PTDrow departmentRow = new PTDrow();
            //departmentRow.PageSize = 100;
            var department = await _projectTechnologyService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTDrow> list2 = new List<PTDrow>();
            if (department != null && department.IsSuccess)
            {
                list2 = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(department.Result));
            }
            model.TechnologyRows = list2;
            var response = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PSDrow> list = new List<PSDrow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(response.Result));
            }
            model.PSDrows = list;
            var response2 = await _projectTypeService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTYDrows> list3 = new List<PTYDrows>();
            if (response != null && response.IsSuccess)
            {
                list3 = JsonConvert.DeserializeObject<List<PTYDrows>>(Convert.ToString(response.Result));
            }
            model.PTYDrows = list3;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProject(ProjectViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            AddEditProjectViewModel model1 = new AddEditProjectViewModel();
            model1.Name = model.Name;
            model.ProjectRow.ProjectCategoryId = 1;
            var result = await _projectService.UpdateAsync<APIResponse>(model.ProjectRow, model.ProjectRow.ProjectId, token);
            if (result.IsSuccess)
            {

                TempData["success"] = "Sucessfully edited project  details";
                return RedirectToAction("Project");
            }

            TempData["failure"] = "Error occured during  editing project  details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;

            return RedirectToAction("Project");
        }

        //Updating project  status
        public async Task<IActionResult> StatusChangeProject(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);

                var result = await _projectService.StatusChange<APIResponse>(Id, Status, token);
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully chaned project status";

                    TempData["failure"] = "Error occured during  updating project status" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                    return RedirectToAction("ProjectStatus");
                }

                return RedirectToAction("Project");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("Project");
            }
        }

        public async Task<IActionResult> AddProject()
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);

            ProjectViewModel model = new ProjectViewModel();
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageSize = 100;

            PTDrow departmentRow = new PTDrow();
            //departmentRow.PageSize = 100;
            var department = await _projectTechnologyService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTDrow> list2 = new List<PTDrow>();
            if (department != null && department.IsSuccess)
            {
                list2 = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(department.Result));
            }
            model.TechnologyRows = list2;
            var response = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PSDrow> list = new List<PSDrow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(response.Result));
            }
            model.PSDrows = list;
            var response2 = await _projectTypeService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTYDrows> list3 = new List<PTYDrows>();
            if (response2 != null && response2.IsSuccess)
            {
                list3 = JsonConvert.DeserializeObject<List<PTYDrows>>(Convert.ToString(response2.Result));
            }
            model.PTYDrows = list3;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            string userId = HttpContext.Session.GetString("userId");
            if (model.CreateProjectViewModel != null && userId != null)
            {

                var data = model.CreateProjectViewModel;
                data.ProjectManagerId = userId;
                data.ProjectCategoryId = 1;
                //data.CreatedDate= DateTime.UtcNow;

                var result = await _projectService.CreateAsync<APIResponse>(data, token);
                if (result != null && result.IsSuccess)
                {
                    return RedirectToAction("Project");
                }
            }
            return View(model);
        }
        #endregion

        #region Task
        //TASK CRUD
        public async Task<IActionResult> AddTaskGet(int Id)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);

            TaskViewModel model = new TaskViewModel();
            model.ProjectId = Id;
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageSize = 100;

            PTDrow departmentRow = new PTDrow();
            //departmentRow.PageSize = 100;
            var response2 = await _projectTypeService.GetAllAsync<APIResponse>(paginationDTO, token);
            List<PTYDrows> list3 = new List<PTYDrows>();
            if (response2 != null && response2.IsSuccess)
            {
                list3 = JsonConvert.DeserializeObject<List<PTYDrows>>(Convert.ToString(response2.Result));
            }
            model.PTYDrows = list3;
            return View("Task/AddTask", model);
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            string userId = HttpContext.Session.GetString("userId");
            if (model != null && userId != null)
            {
                var ps = await _projectService.GetAsync<APIResponse>(model.ProjectId, token);
                if (ps != null && ps.IsSuccess)
                {
                    CreateUpdateTask task = new()
                    {
                        TaskName = model.TaskName,
                        Description = model.Description,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Status = model.Status,
                        Priority = model.Priority,
                        ProjectId = model.ProjectId,
                        TaskCategoryId = model.TaskCategoryId,
                        AssignBy = userId,

                    };
                    var result = await _taskService.CreateAsync<APIResponse>(task, token);
                    if (result != null && result.IsSuccess)
                    {
                        return RedirectToAction("Task/ViewProjectTasks", model.ProjectId);
                    }
                }


            }
            return RedirectToAction("AddTaskGet", model.ProjectId);

        }
        public async Task<IActionResult> ViewProjectTasks(int ProjectId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            string userId = HttpContext.Session.GetString("userId");
            TaskFilter taskFilter = new TaskFilter();
            taskFilter.ProjectId = ProjectId;
            var result = _taskService.GetAllAsync<APIResponse>(taskFilter, token);
            if (result != null)
            {

                var list = JsonConvert.DeserializeObject<List<TaskRow>>(Convert.ToString(result.Result.Result));
                TaskViewModel model = new()
                {
                    ProjectId = ProjectId,
                    TaskRows = list,
                };
                return View("Task/ViewProjectTasks", model);
            }
            return RedirectToAction("Project");
        }
        [HttpPost]
        public async Task<bool> StatusChangeTask(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {
                string token = HttpContext.Session.GetString(SD.SessionToken);

                var result = await _taskService.StatusChange<APIResponse>(Id, Status, token);
                if (result != null && result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully chaned task status";

                    return true;
                }

                return false;
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return false;
            }
        }
        //public async Task<IActionResult> EditTask(int taskId)
        //{
        //    string token = HttpContext.Session.GetString(SD.SessionToken);
        //    var result = _taskService.GetAsync<APIResponse>(taskId, token);
        //    if (result != null)
        //    {

        //        var task = JsonConvert.DeserializeObject<TaskRow>(Convert.ToString(result.Result.Result));

        //        TaskViewModel model = new TaskViewModel();

        //        PaginationDTO paginationDTO = new PaginationDTO();
        //        paginationDTO.PageSize = 100;

        //        PTDrow departmentRow = new PTDrow();
        //        //departmentRow.PageSize = 100;
        //        var response2 = await _projectTypeService.GetAllAsync<APIResponse>(paginationDTO, token);
        //        List<PTYDrows> list3 = new List<PTYDrows>();
        //        if (response2 != null && response2.IsSuccess)
        //        {
        //            list3 = JsonConvert.DeserializeObject<List<PTYDrows>>(Convert.ToString(response2.Result));
        //        }
        //        model.PTYDrows = list3;
        //        model.StartDate = task.StartDate;
        //        model.EndDate = task.EndDate;
        //        model.Description = task.Description;
        //        return View("Task/EditTask", model);
        //    }

        //    return RedirectToAction("Project");
        //}

        //public async Task<bool> MarkAsCompleteTask(int Id)
        //{
        //    string token = HttpContext.Session.GetString(SD.SessionToken);
        //    var result = _taskService.GetAsync<APIResponse>(Id, token);
        //    if (result != null)
        //    {

        //        var task = JsonConvert.DeserializeObject<TaskRow>(Convert.ToString(result.Result.Result));
        //        CreateUpdateTask createUpdateTask = new CreateUpdateTask
        //        {
        //            ProjectId = projectId,  // Replace projectId with the actual ProjectId value
        //            TaskCategoryId = taskCategoryId,  // Replace taskCategoryId with the actual TaskCategoryId value
        //            TaskName = taskRow.TaskName,
        //            StartDate = taskRow.StartDate,
        //            EndDate = taskRow.EndDate,
        //            Priority = Convert.ToInt16(taskRow.Priority),  // Convert Priority from string to short if needed
        //            Description = taskRow.Description,
        //            Status = Convert.ToInt16(taskRow.Status),  // Convert Status from string to short if needed
        //            AssignBy = taskRow.AssignBy
        //        };

        //}




        #endregion

        public async Task<IActionResult> AssignTask(int taskId , string name , int ProjectId)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            EmployeeTaskFilter model = new EmployeeTaskFilter();
            model.TaskId = taskId;
            var result = _employeeTaskService.GetAllAsync<APIResponse>(model, token);
            if (result != null)
            {

                var assigned = JsonConvert.DeserializeObject<List<EmployeeTaskResponseDTO>>(Convert.ToString(result.Result));

                if (assigned.Count == 0)
                {

                    EmployeeFilter employeeFilter = new EmployeeFilter();
                    var result2 = _employeeService.GetAllAsync<APIResponse>(employeeFilter, token);
                    var employees = JsonConvert.DeserializeObject<List<UserResponseDTO>>(Convert.ToString(result.Result.Result));

                    AssignTaskViewModel model2 = new AssignTaskViewModel();
                    model2.Users = employees;
                    model2.TaskId = taskId;
                    model2.TaskName = name;
                    model2.ProjectId = ProjectId;
                    
                    return PartialView("Task/AssignTask", model2);
                }
            }
            return RedirectToAction("Project");
        }
        [HttpPost]
        public async Task<IActionResult> AssignTask(AssignTaskViewModel model)
        {
            string token = HttpContext.Session.GetString(SD.SessionToken);
            string userId = HttpContext.Session.GetString("userId");

            if (model != null)
            {
                List<string> list = new List<string> { model.EmployeeId };
                EmployeeTaskCreateUpdateDTO model2 = new ()
                {
                    TaskId = model.TaskId,
                    ReportTo = userId,
                    EmployeeId =  list,
                };
                var result = _employeeTaskService.CreateAsync<APIResponse>(model2, token);
                if(result!=null && result.Result.IsSuccess)
                {
                    TempData["success"] = "Successfully assigned employee to the task";
                    return RedirectToAction("ViewProjectTasks", new { ProjectId = model.ProjectId } );
                }

            }
            TempData["failure"] = "Error assigning employee to the task";
            return RedirectToAction("ViewProjectTasks", new { ProjectId = model.ProjectId });

        }

    }
}
