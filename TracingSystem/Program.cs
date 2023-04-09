using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TracingSystem.Application;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Domain;
using TracingSystem.Persistance;

namespace TracingSystem
{
    internal static class Program
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        static void DependencyInjectionConfig()
        {
            ServiceProvider = new ServiceCollection()
                .AddPersistance()
                .AddApplication()
                .BuildServiceProvider();
        }

        static void ShutDown()
        {
            ServiceProvider?.Dispose();
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DependencyInjectionConfig();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            var dbContext = ServiceProvider.GetRequiredService<TracingSystemDbContext>();
            DbInitializer.Initialize(dbContext);

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();
            System.Windows.Forms.Application.Run(new MainForm());
            ShutDown();
        }
    }
}