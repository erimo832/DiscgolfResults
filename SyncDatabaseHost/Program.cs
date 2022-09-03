using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Results.Domain;
using Results.Domain.Configuration;
using Results.Domain.Service;

namespace DbTest
{
    public class Program
    {
        /*
         
        TODO:
        
        - Update ctp                
        - Add automatic binding to course and layout
        
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

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Service.Sync();

            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}