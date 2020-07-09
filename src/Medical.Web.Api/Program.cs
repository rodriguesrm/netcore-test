using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Medical.Web.Api
{

    /// <summary>
    /// Class to run application
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args">Run execute parameters</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create host builder application
        /// </summary>
        /// <param name="args">Run execute parameters</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
