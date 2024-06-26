using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectManagement_UI.Models;
using ProjectManagement_UI.Models.Auth;
using ProjectManagement_UI.Models.Department;
using ProjectManagement_UI.Models.Designation;
using ProjectManagement_UI.Services.GenericMasterServices;
using ProjectManagement_UI.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProjectManagement_UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDepartmentService _departmentService;
        private readonly IDesignationService _designationService;
        public AuthController(IAuthService authService, IHttpContextAccessor httpContextAccessor , IDepartmentService departmentService, IDesignationService designationService)
        {
            _designationService = designationService;
            _departmentService = departmentService;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel obj)
        {
            APIResponse response = await _authService.LoginAsync<APIResponse>(obj);
            if (response != null && response.Result != null && response.IsSuccess)
            {
                //LoginResponseModel model = JsonConvert.DeserializeObject<LoginResponseModel>(Convert.ToString(response.Result));

                var jwttoken = response.Result;
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(response.Result.ToString());

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == "userName").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "roleId").Value));
                identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value));
                identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == "userId").Value));

                Response.Cookies.Append("pms", response.Result.ToString());

                //identity.AddClaim(new Claim(ClaimTypes., jwt.Claims.FirstOrDefault(u => u.Type == "roleId").Value));
                _httpContextAccessor.HttpContext.Session.SetString("Email", jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value);
                _httpContextAccessor.HttpContext.Session.SetString("UserName", jwt.Claims.FirstOrDefault(u => u.Type == "userName").Value);
                _httpContextAccessor.HttpContext.Session.SetString("RoleId", jwt.Claims.FirstOrDefault(u => u.Type == "roleId").Value);
                _httpContextAccessor.HttpContext.Session.SetString(SD.SessionToken, jwttoken.ToString());

                var sd = _httpContextAccessor.HttpContext.Session.GetString(SD.SessionToken);

                TempData["success"] = "Succesfully Logged In.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["failure"] = "Data not found";
                return View(obj);
            }
        }
        public IActionResult Logout()
        {

            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("RoleId");
            HttpContext.Session.Remove(SD.SessionToken);
            HttpContext.Session.Clear();
            Response.Cookies.Delete("pms");

            return RedirectToAction("Login", "Auth");

        }
        public async Task<IActionResult> Registration()
        {
            PaginationDTO paginationDTO = new PaginationDTO();
            paginationDTO.PageSize = 100;
            RegistrationViewModel model = new RegistrationViewModel();

            var designation = await _designationService.GetAllAsync<APIResponse>(paginationDTO);
            List<DesignationRow> list1 = new List<DesignationRow>();
            if (designation != null && designation.IsSuccess)
            {
                list1 = JsonConvert.DeserializeObject<List<DesignationRow>>(Convert.ToString(designation.Result));
            }
            model.designationRows = list1;

            DepartmentFilterRequestDTO departmentRow = new DepartmentFilterRequestDTO();
            departmentRow.PageSize = 100;
            var department = await _departmentService.GetAllAsync<APIResponse>(departmentRow);
            List<DepartmentRow> list2 = new List<DepartmentRow>();
            if (department != null && department.IsSuccess)
            {
                list1 = JsonConvert.DeserializeObject<List<DesignationRow>>(Convert.ToString(department.Result));
            }
            model.departmentRows = list2;

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Registration(RegistrationViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //}
    }
}
