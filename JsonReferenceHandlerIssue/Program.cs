using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JsonReferenceHandlerIssue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = $"{Path.Combine(Environment.CurrentDirectory, "nlog.config")}";
            NLog.LogManager.LogFactory.SetCandidateConfigFilePaths(new List<string> { path });
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();
                host.Services.GetRequiredService<ILogger<Startup>>().LogInformation($"JsonReferenceHandlerIssue version {GetVersion()}");
                host.Services.GetRequiredService<ILogger<Startup>>().LogInformation($"Logging configuration: {path}");
                host.Run();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();


        private static string GetVersion()
        {
            var versionAttribute = typeof(Program).Assembly
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), true)
                .FirstOrDefault() as AssemblyInformationalVersionAttribute;

            return versionAttribute?.InformationalVersion ?? typeof(Program).Assembly.GetName().Version.ToString();
        }

    }
}
