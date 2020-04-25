using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TichTich.Web.Areas.Identity.IdentityHostingStartup))]

namespace TichTich.Web.Areas.Identity
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