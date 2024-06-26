using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Models.ProjectStatus;
using ProjectManagement_UI.Models.ProjectTechnology;
using ProjectManagement_UI.Models.ProjectType;
using ProjectManagement_UI.Services.AuthServices;
using ProjectManagement_UI.Services.GenericMasterServices;
using ProjectManagement_UI.Services.IServices;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectManagement_UI.Controllers
{
    [CustomAuthorize(1)]
    public class GenericMasterController : Controller
    {
        private readonly ILogger<GenericMasterController> _logger;
        private readonly IProjectStatusService _projectStatusService;
        private readonly IProjectTechnologyservice _projectTechnologyService;
        private readonly IProjectTypeService _projectTypeService;
        private readonly IDepartmentService _departmentService;
        private readonly IDesignationService _designationService;
        public GenericMasterController(ILogger<GenericMasterController> logger, IProjectStatusService projectStatusService, IProjectTechnologyservice projectTechnologyservice, IProjectTypeService projectTypeService, IDepartmentService departmentService, IDesignationService designationService)
        {
            _departmentService = departmentService;
            _projectTechnologyService = projectTechnologyservice;
            _projectStatusService = projectStatusService;
            _logger = logger;
            _projectTypeService = projectTypeService;
            _designationService = designationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #region ProjectStatus

        //Getting all the project status data 
        [HttpGet]
        public async Task<ActionResult> ProjectStatus()
        {

            ProjectStatusDashboardViewModel model = new();
            model.CurrentPage = 1;
            return View("ProjectStatus/ProjectStatus", model);
        }

        public async Task<IActionResult> ProjectStatusPartial(int page, string searchFilter)
        {
            int pageNumber = 1;
            if (page > 0)
            {
                pageNumber = page;
            }
            ProjectStatusDashboardViewModel model = new ProjectStatusDashboardViewModel();
            model.PSDrows = await GetPSRequestsAsync(searchFilter, pageNumber);
            if (pageNumber > model.PSDrows.Count() / 5)
            {
                model.CurrentPage = model.PSDrows.Count() / 5 + 1;
            }
            else
            {
                model.CurrentPage = pageNumber;

            }

            return PartialView("ProjectStatus/_ProjectStatusPartial", model);
        }

        public async Task<List<PSDrow>> GetPSRequestsAsync(string searchFilter, int pageNumber)
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.Name = searchFilter;
            paginationDTO.PageNumber = pageNumber;
            var response = await _projectStatusService.GetAllAsync<APIResponse>(paginationDTO);
            List<PSDrow> list = new List<PSDrow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PSDrow>>(Convert.ToString(response.Result));
            }
            return list;
        }

        //Create project status view modal 
        public IActionResult AddPSModal()
        {
            AddEditPSViewModel model = new AddEditPSViewModel();

            return PartialView("ProjectStatus/_AddProjectStatus", model);
        }
        //Create project status post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProjectStatus(AddEditPSViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _projectStatusService.CreateAsync<APIResponse>(model);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added project status details";

                    return RedirectToAction("ProjectStatus");
                }
                TempData["failure"] = "Error occured during  adding project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectStatus");
        }

        //Getting single project status information
        public async Task<IActionResult> EditProjectStatus(int StudentId)
        {

            var ps = await _projectStatusService.GetAsync<APIResponse>(StudentId);

            if (ps == null)
            {
                TempData["failure"] = "Project status not found";
                return View("e");
            }
            PSDrow pSDrow = new PSDrow();
            pSDrow = JsonConvert.DeserializeObject<PSDrow>(Convert.ToString(ps.Result));

            return PartialView("ProjectStatus/_EditProjectStatus", pSDrow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjectStatus(PSDrow model)
        {

            if (ModelState.IsValid)
            {
                AddEditPSViewModel addEditPSViewModel = new AddEditPSViewModel();
                addEditPSViewModel.Name = model.Name;
                var result = await _projectStatusService.UpdateAsync<APIResponse>(addEditPSViewModel, model.StatusId);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully edited project status details";
                    return RedirectToAction("ProjectStatus");
                }

                TempData["failure"] = "Error occured during  editing project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectStatus");
        }
        //Deleting project status 
        public async Task<IActionResult> DeleteProjectStatus(int id)
        {
            var result = await _projectStatusService.DeleteAsync<APIResponse>(id);
            if (result.IsSuccess)
            {
                TempData["success"] = "Sucessfully added students details";

                return RedirectToAction("ProjectStatus");
            }

            TempData["failure"] = "Error occured during  deleting project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            return RedirectToAction("ProjectStatus");
        }
        public async Task<IActionResult> StatusChangePS(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {

                var result = await _projectStatusService.StatusChange<APIResponse>(Id, Status);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully added students details";

                    return RedirectToAction("ProjectStatus");
                }

                TempData["failure"] = "Error occured during  deleting project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                return RedirectToAction("ProjectStatus");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("ProjectStatus");
            }
        }

        #endregion

        #region ProjectTechnology

        //Getting all the project technology data 
        [HttpGet]
        public ActionResult ProjectTechnology()
        {

            ProjectTechnologyDashboardViewModel model = new();
            model.CurrentPage = 1;
            return View("ProjectTechnology/ProjectTechnology", model);
        }

        public async Task<IActionResult> ProjectTechnologyPartial(int page, string searchFilter)
        {
            int pageNumber = 1;
            if (page > 0)
            {
                pageNumber = page;
            }
            ProjectTechnologyDashboardViewModel model = new ProjectTechnologyDashboardViewModel();
            model.PTDrows = await GetPTRequestsAsync(searchFilter, pageNumber);
            if (pageNumber > model.PTDrows.Count() / 5)
            {
                model.CurrentPage = model.PTDrows.Count() / 5 + 1;
            }
            else
            {
                model.CurrentPage = pageNumber;

            }

            return PartialView("ProjectTechnology/_ProjectTechnologyPartial", model);
        }

        public async Task<List<PTDrow>> GetPTRequestsAsync(string searchFilter, int pageNumber)
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.Name = searchFilter;
            paginationDTO.PageNumber = pageNumber;
            var response = await _projectTechnologyService.GetAllAsync<APIResponse>(paginationDTO);
            List<PTDrow> list = new List<PTDrow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PTDrow>>(Convert.ToString(response.Result));
            }
            return list;
        }

        //Create project technology view modal 
        public IActionResult AddPTModal()
        {
            AddEditPTViewModel model = new AddEditPTViewModel();

            return PartialView("ProjectTechnology/_AddProjectTechnology", model);
        }
        //Create project technology post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProjectTechnology(AddEditPTViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _projectTechnologyService.CreateAsync<APIResponse>(model);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added project technology details";

                    return RedirectToAction("ProjectTechnology");
                }
                TempData["failure"] = "Error occured during  adding project technology details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectTechnology");
        }
        //Updating project technology status
        public async Task<IActionResult> StatusChangePT(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {

                var result = await _projectTechnologyService.StatusChange<APIResponse>(Id, Status);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully added students details";

                    return RedirectToAction("ProjectStatus");
                }

                TempData["failure"] = "Error occured during  deleting project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                return RedirectToAction("ProjectStatus");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("ProjectStatus");
            }
        }
        //Getting single project status information
        //[HttpGet("EditProjectTechnology/{Id}")]
        [HttpGet]
        public async Task<IActionResult> EditProjectTechnology(int Id)
        {
            if (Id == 0)
            {
                TempData["failure"] = "Project technology not found";
                return View("ProjectTechnology/_EditProjectTechnology");
            }
            var pt = await _projectTechnologyService.GetAsync<APIResponse>(Id);


            if (pt == null)
            {
                TempData["failure"] = "Project technology not found";
                return View("ProjectTechnology/_EditProjectTechnology");
            }
            PTDrow pSDrow = new();
            pSDrow = JsonConvert.DeserializeObject<PTDrow>(Convert.ToString(pt.Result));

            return PartialView("ProjectTechnology/_EditProjectTechnology", pSDrow);
        }
        //edit project technology modal 
        [HttpPost("EditProjectTechnology")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjectTechnology(PTDrow model)
        {

            if (ModelState.IsValid)
            {
                AddEditPTViewModel addEditPSViewModel = new AddEditPTViewModel();
                addEditPSViewModel.Name = model.Name;
                var result = await _projectTechnologyService.UpdateAsync<APIResponse>(addEditPSViewModel, model.Id);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added project technology details";
                    return RedirectToAction("ProjectTechnology");
                }

                TempData["failure"] = "Error occured during  editing project technology details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectTechnology");
        }
        //deleting project technology
        public async Task<IActionResult> DeleteProjectTechnology(int id)
        {
            if (id != 0)
            {

                var result = await _projectTechnologyService.DeleteAsync<APIResponse>(id);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully deleted project technology details";

                    return RedirectToAction("ProjectTechnology");
                }

                TempData["failure"] = "Error occured during  deleting project technology details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectTechnology");
        }
        #endregion

        #region ProjectType
        //Getting all the project technology data 
        [HttpGet]
        public ActionResult ProjectType()
        {

            ProjectTechnologyDashboardViewModel model = new();
            model.CurrentPage = 1;
            return View("ProjectType/ProjectType", model);
        }

        public async Task<IActionResult> ProjectTypePartial(int page, string searchFilter)
        {
            int pageNumber = 1;
            if (page > 0)
            {
                pageNumber = page;
            }
            ProjectTypeDashboardViewModel model = new ProjectTypeDashboardViewModel();
            model.PTYDrows = await GetPTYRequestsAsync(searchFilter, pageNumber);
            if (pageNumber > model.PTYDrows.Count() / 5)
            {
                model.CurrentPage = model.PTYDrows.Count() / 5 + 1;
            }
            else
            {
                model.CurrentPage = pageNumber;

            }

            return PartialView("ProjectType/_ProjectTypePartial", model);
        }

        public async Task<List<PTYDrows>> GetPTYRequestsAsync(string searchFilter, int pageNumber)
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.Name = searchFilter;
            paginationDTO.PageNumber = pageNumber;
            var response = await _projectTypeService.GetAllAsync<APIResponse>(paginationDTO);
            List<PTYDrows> list = new List<PTYDrows>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<PTYDrows>>(Convert.ToString(response.Result));
            }
            return list;
        }

        //Create project technology view modal 
        public IActionResult AddPTYModal()
        {
            AddEditPTYViewModel model = new AddEditPTYViewModel();

            return PartialView("ProjectType/_AddProjectType", model);
        }
        //Create project technology post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProjectType(AddEditPTYViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _projectTypeService.CreateAsync<APIResponse>(model);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added project type details";

                    return RedirectToAction("ProjectType");
                }
                TempData["failure"] = "Error occured during  adding project type details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectType");
        }
        //Updating project technology status
        public async Task<IActionResult> StatusChangePTY(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {

                var result = await _projectTypeService.StatusChange<APIResponse>(Id, Status);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully added project type details";

                    return RedirectToAction("ProjectStatus");
                }

                TempData["failure"] = "Error occured during  deleting project type details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                return RedirectToAction("ProjectType");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("ProjectType");
            }
        }
        //Getting single project status information
        //not-working
        //[HttpGet("EditProjectTechnology/{Id}")]
        public async Task<IActionResult> EditProjectType(int Id)
        {
            if (Id == 0)
            {
                TempData["failure"] = "Project type not found";
                return View("ProjectType/_EditProjectType");
            }
            var pt = await _projectTypeService.GetAsync<APIResponse>(Id);


            if (pt == null)
            {
                TempData["failure"] = "Project type not found";
                return View("ProjectType/_EditProjectType");
            }
            PTYDrows pSDrows = new PTYDrows();
            pSDrows = JsonConvert.DeserializeObject<PTYDrows>(Convert.ToString(pt.Result));

            return PartialView("ProjectType/_EditProjectType", pSDrows);
        }
        //edit project technology modal 
        //not-tried
        [HttpPost("EditProjectType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjectType(PTYDrows model)
        {

            if (ModelState.IsValid)
            {
                AddEditPTYViewModel addEditPSViewModel = new AddEditPTYViewModel();
                addEditPSViewModel.Name = model.Name;
                var result = await _projectTypeService.UpdateAsync<APIResponse>(addEditPSViewModel, model.ProjectCategoryId);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added project type details";
                    return RedirectToAction("ProjectType");
                }

                TempData["failure"] = "Error occured during  editing project type details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectType");
        }
        //deleting project technology
        public async Task<IActionResult> DeleteProjectType(int id)
        {
            if (id != 0)
            {

                var result = await _projectTypeService.DeleteAsync<APIResponse>(id);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully deleted project type details";

                    return RedirectToAction("ProjectType");
                }

                TempData["failure"] = "Error occured during  deleting project type details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("ProjectType");
        }
        #endregion

        #region Department

        //Getting all the project status data 
        [HttpGet]
        public async Task<ActionResult> Department()
        {

            DepartmentDashboardViewModel model = new();
            model.CurrentPage = 1;
            return View("Department/Department", model);
        }

        public async Task<IActionResult> DepartmentPartial(int page, string searchFilter)
        {
            int pageNumber = 1;
            if (page > 0)
            {
                pageNumber = page;
            }
            DepartmentDashboardViewModel model = new DepartmentDashboardViewModel();
            model.DPRows = await GetDepartmentRequestsAsync(searchFilter, pageNumber);
            if (pageNumber > model.DPRows.Count() / 5)
            {
                model.CurrentPage = model.DPRows.Count() / 5 + 1;
            }
            else
            {
                model.CurrentPage = pageNumber;

            }

            return PartialView("Department/_DepartmentPartialView", model);
        }

        public async Task<List<DepartmentRow>> GetDepartmentRequestsAsync(string searchFilter, int pageNumber)
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.Name = searchFilter;
            paginationDTO.PageNumber = pageNumber;
            DepartmentFilterRequestDTO departmentRow    = new DepartmentFilterRequestDTO();
            departmentRow.Name = searchFilter;
            departmentRow.PageNumber = pageNumber;
            var response = await _departmentService.GetAllAsync<APIResponse>(departmentRow);
            List<DepartmentRow> list = new List<DepartmentRow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DepartmentRow>>(Convert.ToString(response.Result));
            }
            return list;
        }

        //Create department view modal 
        [HttpGet]
        public IActionResult AddDepartment()
        {
            AddEditDepartmentViewModel model = new AddEditDepartmentViewModel();

            return PartialView("Department/_AddDepartment", model);
        }
        //Create project status post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(AddEditDepartmentViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _departmentService.CreateAsync<APIResponse>(model);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added students details";

                    return RedirectToAction("Department");
                }
                TempData["failure"] = "Error occured during  adding project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("Department");
        }

        //Getting single project status information
        [HttpGet]
        public async Task<IActionResult> EditDepartmentViewModel(int Id)
        {

            var ps = await _departmentService.DeleteAsync<APIResponse>(Id);

            if (ps == null)
            {
                TempData["failure"] = "Project status not found";
                return View("e");
            }
            DepartmentRow pSDrow = new DepartmentRow();
            pSDrow = JsonConvert.DeserializeObject<DepartmentRow>(Convert.ToString(ps.Result));

            return PartialView("Department/_EditDepartment", pSDrow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(DepartmentRow model)
        {

            if (ModelState.IsValid)
            {
                EditDepartment addEditPSViewModel = new EditDepartment();
                addEditPSViewModel.Name = model.Name;
                addEditPSViewModel.DepartmentId= model.DepartmentId;
                var result = await _departmentService.UpdateAsync<APIResponse>(addEditPSViewModel);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added students details";
                    return RedirectToAction("Department");
                }

                TempData["failure"] = "Error occured during  editing project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("Department");
        }
        //minor error
        //Deleting project status 
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await _departmentService.DeleteAsync<APIResponse>(id);
            if (result.IsSuccess)
            {
                TempData["success"] = "Sucessfully added students details";

                return RedirectToAction("Department");
            }

            TempData["failure"] = "Error occured during  deleting project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            return RedirectToAction("Department");
        }
        public async Task<IActionResult> StatusChangeDepartment(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {

                var result = await _departmentService.StatusChange<APIResponse>(Id, Status);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully added students details";

                    return RedirectToAction("Department");
                }

                TempData["failure"] = "Error occured during  deleting project status details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                return RedirectToAction("Department");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("Department");
            }
        }
        #endregion

        #region Designation

        //Getting all the project status data 
        [HttpGet]
        public async Task<ActionResult> Designation()
        {

            DesignationDashboardViewModel model = new();
            model.CurrentPage = 1;
            return View("Designation/Designation", model);
        }

        public async Task<IActionResult> DesignationPartial(int page, string searchFilter)
        {
            int pageNumber = 1;
            if (page > 0)
            {
                pageNumber = page;
            }
            DesignationDashboardViewModel model = new DesignationDashboardViewModel();
            model.DSRows = await GetDesignationtRequestsAsync(searchFilter, pageNumber);
            if (pageNumber > model.DSRows.Count() / 5)
            {
                model.CurrentPage = model.DSRows.Count() / 5 + 1;
            }
            else
            {
                model.CurrentPage = pageNumber;

            }

            return PartialView("Designation/_DesignationPartialView", model);
        }

        public async Task<List<DesignationRow>> GetDesignationtRequestsAsync(string searchFilter, int pageNumber)
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.Name = searchFilter;
            paginationDTO.PageNumber = pageNumber;
            //DesignationDashboardViewModel departmentRow = new DesignationDashboardViewModel();
            //departmentRow.Name = searchFilter;
            //departmentRow.PageNumber = pageNumber;
            var response = await _designationService.GetAllAsync<APIResponse>(paginationDTO);
            List<DesignationRow> list = new List<DesignationRow>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<DesignationRow>>(Convert.ToString(response.Result));
            }
            return list;
        }

        //Create department view modal 
        [HttpGet]
        public IActionResult AddDesignation()
        {
            AddEditDepartmentViewModel model = new AddEditDepartmentViewModel();

            return PartialView("Designation/_AddDesignation", model);
        }
        //Create project status post method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDesignation(AddEditDesignationViewModel model)
        {

            if (ModelState.IsValid)
            {

                var result = await _designationService.CreateAsync<APIResponse>(model);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added Designation details";

                    return RedirectToAction("Department");
                }
                TempData["failure"] = "Error occured during  adding Designation details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("Department");
        }

        //Getting single project status information
        [HttpGet]
        public async Task<IActionResult> EditDesignation(int Id)
        {

            var ps = await _designationService.DeleteAsync<APIResponse>(Id);

            if (ps == null)
            {
                TempData["failure"] = "Designation not found";
                return View("e");
            }
            DesignationRow pSDrow = new DesignationRow();
            pSDrow = JsonConvert.DeserializeObject<DesignationRow>(Convert.ToString(ps.Result));

            return PartialView("Designation/_EditDesignation", pSDrow);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDesignation(DesignationRow model)
        {

            if (ModelState.IsValid)
            {
                EditDesignation addEditPSViewModel = new EditDesignation();
                addEditPSViewModel.Name = model.Name;
                addEditPSViewModel.DesignationId = model.DesignationId;
                var result = await _designationService.UpdateAsync<APIResponse>(addEditPSViewModel, model.DesignationId);
                if (result.IsSuccess)
                {

                    TempData["success"] = "Sucessfully added designation";
                    return RedirectToAction("Designation");
                }

                TempData["failure"] = "Error occured during  editing Designation details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            }
            return RedirectToAction("Designation");
        }
        //minor error
        //Deleting project status 
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var result = await _designationService.DeleteAsync<APIResponse>(id);
            if (result.IsSuccess)
            {
                TempData["success"] = "Sucessfully deleted Designation details";

                return RedirectToAction("Designation");
            }

            TempData["failure"] = "Error occured during  deleting Designation details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
            return RedirectToAction("Designation");
        }
        public async Task<IActionResult> StatusChangeDesignation(int Id, bool Status)
        {
            if (Id != 0 && Status != null)
            {

                var result = await _designationService.StatusChange<APIResponse>(Id, Status);
                if (result.IsSuccess)
                {
                    TempData["success"] = "Sucessfully added Designation";

                    return RedirectToAction("Designation");
                }

                TempData["failure"] = "Error occured during  deleting Designation details" + " ErrorMessage: " + result.ErrorMessages + " Statuscode: " + result.StatusCode;
                return RedirectToAction("Designation");
            }
            else
            {
                TempData["failure"] = "Please enter id or status";
                return RedirectToAction("Designation");
            }
        }
        #endregion

    }
}
