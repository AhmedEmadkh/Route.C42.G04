using Demo.BLL.Common.Services.Attachment;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repositories.Departments;
using Demo.DAL.Presistance.Repositories.Employees;
using Demo.DAL.Presistance.UnitOfWork;
using Demo.PL.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
