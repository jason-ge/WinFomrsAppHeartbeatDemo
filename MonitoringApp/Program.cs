using Heartbeat.Abstractions;
using HeartbeatServerService;
using HeartBeatService.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MonitoringApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();
            host.RunAsync();
            Application.Run(host.Services.GetRequiredService<MonitorForm>());
        }

        static IHostBuilder CreateHostBuilder()
        {
            int _heartbeatInterval = 5;
            return Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, configBuilder) => {
                    var configuration = configBuilder.AddJsonFile("appsettings.json").Build();
                    if (!int.TryParse(configuration["heartbeat:intervalInSeconds"], out _heartbeatInterval))
                    {
                        _heartbeatInterval = 5;
                    }
                })
                .ConfigureServices((context, services) => {
                    services.AddSingleton<IHeartbeatMonitorChannel, AnonymousPipeServerChannel>((services) => new AnonymousPipeServerChannel(_heartbeatInterval));
                    services.AddSingleton<HeartbeatMonitorService>();
                    services.AddHostedService(sp => sp.GetRequiredService<HeartbeatMonitorService>());
                    services.AddTransient((sp) => new MonitorForm(sp, _heartbeatInterval));
                });
        }
    }
}
