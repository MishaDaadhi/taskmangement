using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((host, builder) =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                var config = builder.Build();

                if (config.GetValue("UseVault", false))
                {
                    builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"),new DefaultAzureCredential());// ($"https://{config["Vault:Name"]}.vault.azure.net/", config["Vault:ClientId"], config["Vault:ClientSecret"]);
                }
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((host, builder) =>
                {
                    builder.MinimumLevel.Verbose()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .Enrich.WithProperty("ApplicationContext", host.HostingEnvironment.ApplicationName)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.Seq(string.IsNullOrWhiteSpace(host.Configuration["Serilog:SeqServerUrl"]) ? "http://seq" : host.Configuration["Serilog:SeqServerUrl"])
                        .WriteTo.Http(string.IsNullOrWhiteSpace(host.Configuration["Serilog:LogstashgUrl"]) ? "http://logstash:8080" : host.Configuration["Serilog:LogstashgUrl"],500)
                        .ReadFrom.Configuration(host.Configuration);
                });
    }
}
