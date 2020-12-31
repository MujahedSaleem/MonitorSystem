using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MonitorSystem.Areas.Identity.IdentityHostingStartup))]
namespace MonitorSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}