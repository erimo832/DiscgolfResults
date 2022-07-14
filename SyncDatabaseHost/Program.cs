using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Results.Domain;
using Results.Domain.Service;

namespace DbTest
{
    public class Program
    {
        /*
         
        TODO:
        x Export all to xlsx
        x Add support xlsx
        x Add calculations of Hcp for round (48 = 0, 49 = 0,8 etc)
        x Keep log of current hcp for each round?
        x How to handle hcp and hcp score. 
        x PlayerEvent add calculation and storage
        - Add duplicate alias support
        x Add support multiple players same name
        - Export Skövde to xlsx

        
        - Update ctp
                
        - Add automatic binding to course and layout
        - Move code to git project
        - Update Api
        - ...


         */

        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
              .ConfigureAppConfiguration((hostingContext, config) =>
              {
                  /*
                  config.AddJsonFile("appsettings.json", optional: true);
                  config.AddEnvironmentVariables();

                  if (args != null)
                  {
                      config.AddCommandLine(args);
                  }*/
              })
              .ConfigureServices((hostContext, services) =>
              {
                  services.AddResultBackend(hostContext.Configuration);
                  services.AddSingleton<IHostedService, SyncService>();
                  /*
                  services.AddOptions();
                  services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                  services.AddSingleton<IHostedService, PrintTextToConsoleService>();*/
              })
              /*
              .ConfigureLogging((hostingContext, logging) =>
              {
                  logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                  logging.AddConsole();
              })*/
              ;

            await builder.RunConsoleAsync();
        }
    }

    public class SyncService : IHostedService
    {
        private ISyncDatabaseService Service { get; }

        public SyncService(ISyncDatabaseService service)
        {
            Service = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Service.Sync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}