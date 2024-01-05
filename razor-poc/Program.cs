using System;

namespace razor_poc
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            return Host.CreateDefaultBuilder(args)
             .ConfigureAppConfiguration((app, config) =>
             {
                 if (isDevelopment)
                 {
                     config.AddJsonFile($"appSettings.{Environment.MachineName}.json", optional: true);
                 }
                 else
                 {
                     Console.WriteLine(@"HostingEnvironmentName: '{0}'", environment);
                     config.AddJsonFile($"appSettings.{environment}.json", true, true);
                     config.AddEnvironmentVariables();
                 }
             })
                .ConfigureCmsDefaults()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
