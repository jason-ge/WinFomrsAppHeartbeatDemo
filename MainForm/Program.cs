using Heartbeat.Abstractions;
using HeartbeatChannel.Client;
using HeartbeatClientService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MainForm
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (args.Length != 2)
            {
                throw new ArgumentException("We need tw parameters (anonymous pipe handle and heartbeat interval) to run the app.");
            }
            var host = CreateHostBuilder(args[0], int.Parse(args[1])).Build();
            host.RunAsync();
            Application.Run(host.Services.GetRequiredService<Form1>());
        }

        static IHostBuilder CreateHostBuilder(string pipeHandler, int heartbeatInterval)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddSingleton<IHeartbeatClientChannel, AnonymousPipeClientChannel>((services) => new AnonymousPipeClientChannel(pipeHandler, heartbeatInterval));
                    services.AddSingleton<HeartbeatGeneratorService>();
                    services.AddHostedService(sp => sp.GetRequiredService<HeartbeatGeneratorService>());
                    services.AddTransient<Form1>();
                });
        }
    }
}