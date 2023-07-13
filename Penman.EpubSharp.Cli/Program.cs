using CommandLine;
using System.Reflection;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Penman.EpubSharp.Cli.ActionHandlers;

namespace Penman.EpubSharp.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            var result = host.Services.GetService<ICommandHandler>()
                .ExecuteAsync(args).Result;

          
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).UseLamar((_, registry) =>
            {
                registry.AddLogging();

                registry.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.AssemblyContainingType<Program>();
                    s.AssemblyContainingType(typeof(Program));
                    s.WithDefaultConventions();
                    s.LookForRegistries();
                });
            });
        }
    }
}