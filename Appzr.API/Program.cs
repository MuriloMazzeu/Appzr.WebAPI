using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace Appzr.API
{
    public abstract class Program
    {
        public static async Task Main(string[] args)
        {
            using (var webHost = CreateWebHostBuilder(args).Build())
            {
                await webHost.RunAsync();
            }
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
