using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GovernanceReminderMails
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {
            // Create service collection
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create service provider
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                 await serviceProvider.GetService<GovernanceService>().Run();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfiguration
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            // Add app
            serviceCollection.AddTransient<GovernanceService>();
            serviceCollection.AddSingleton<Repository>();
            serviceCollection.AddSingleton<EmailService>();
        }
    }
}
