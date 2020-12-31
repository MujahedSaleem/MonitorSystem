using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MonitorSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Task.Run(async () =>
            { var scope = host.Services.CreateScope();
                var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var Email = config.GetSection("AppSetting")["Email"];
                var UserName = config.GetSection("AppSetting")["UserName"];
                var pass = config.GetSection("AppSetting")["Password"];
                var user = await userManager.FindByEmailAsync(Email);
                if (user == null)
                    await userManager.CreateAsync(new IdentityUser() { Email = Email, UserName = UserName }, pass);
                scope.Dispose();
              

            }).Wait();
            host.StartAsync().Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
