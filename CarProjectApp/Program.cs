using CarProjectApp.Repositories;
using CarProjectApp.Repositories.Interfaces;
using CarProjectApp.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarProjectApp
{
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Server=DESKTOP-S5D5S14\\SQLEXPRESS; Initial Catalog=CarDB; Trusted_Connection = True;");
            });
            services.AddScoped<ICarRepository, CarRepository>();
            ServiceProvider = services.BuildServiceProvider();
            DbInitializer.Seed(ServiceProvider.GetRequiredService<ApplicationDbContext>());
        }

        [STAThread]
        static void Main()
        {
            ConfigureServices();
            Console.WriteLine("Hello World");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
