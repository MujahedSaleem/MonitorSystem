using Blazored.Toast;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonitorSystem.Areas.Identity;
using MonitorSystem.Data;
using MonitorSystem.Service;
using System;
using System.Threading.Tasks;
using DevExpress.Blazor.Reporting;
using Microsoft.AspNetCore.Http;
using DevExpress.XtraReports.Web.Extensions;
namespace MonitorSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBlazorise(options =>
                {
                    options.DelayTextOnKeyPress = true;
                    options.DelayTextOnKeyPressInterval = 700;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.SignIn.RequireConfirmedEmail = false;

                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSignalR(e =>

            {

                e.MaximumReceiveMessageSize = 102400000;

            });
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddBlazoredToast();
            services.AddTransient<IContractorService, ContractorService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IMovmentService, MonementService>();
            services.AddTransient<IExcelService, ExcelService>();
            services.AddScoped(typeof(IPrintService<>), typeof(PrintService<>));
            services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddTransient(typeof(Lazy<>), typeof(Lazy<>));
            services.AddDevExpressBlazorReporting();
            services.AddScoped<DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension, ReportStorageWebExtension>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.ApplicationServices
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();
            app.UseDevExpressBlazorReporting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
       
    }
}
