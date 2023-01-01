using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Monitoring_FrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace Monitoring_FrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}