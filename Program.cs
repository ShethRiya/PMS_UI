using Domain.Auth.Interface;
using Infrastructure.Auth.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectManagement_UI.Services.AuthServices;
using ProjectManagement_UI.Services.GenericMasterServices;
using ProjectManagement_UI.Services.IServices;
using ProjectManagement_UI.Services.ProjectServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IProjectStatusService, ProjectStatusSrevice>();
builder.Services.AddScoped<IProjectStatusService, ProjectStatusSrevice>();
//builder.Services.AddHttpClient<IProjectTechnologyservice, ProjectTechnologyService>();
builder.Services.AddScoped<IProjectTechnologyservice, ProjectTechnologyService>();
builder.Services.AddHttpClient<IProjectTypeService, ProjectTypeService>();
builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
builder.Services.AddHttpClient<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddHttpClient<IDesignationService, DesignationService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHttpClient<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IJwtRepository, JwtRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".HalloDoc.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
