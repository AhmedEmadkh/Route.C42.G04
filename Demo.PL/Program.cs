using Demo.BLL.Common.Services.Attachment;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Identity;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Presistance.UnitOfWork;
using Demo.PL.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services

            webApplicationBuilder.Services.AddControllersWithViews();
            webApplicationBuilder.Logging.AddConsole();
            webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                .UseLazyLoadingProxies()
                .UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //webApplicationBuilder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //webApplicationBuilder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            webApplicationBuilder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



            webApplicationBuilder.Services.AddScoped<IDepartmentService, DepartmentService>();
            webApplicationBuilder.Services.AddScoped<IEmployeeService, EmployeeService>();
            webApplicationBuilder.Services.AddTransient<IAttachmentService, AttachmentService>();

            webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;


                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();


            webApplicationBuilder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Acount/SignIn";
            });


            // Configure the Auto Mapping
            webApplicationBuilder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));
            #endregion

            var app  = webApplicationBuilder.Build();

            #region Configure

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            //app.MapGet("/", async context =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});
            
            #endregion

            app.Run();


        }
    }
}
