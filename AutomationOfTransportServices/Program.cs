using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutomationOfTransportServices;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var configuration = BuildConfiguration();
        var connectionString = configuration.GetConnectionString("DbConnection");
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddDbContext<TransportServicesDbContext>(options => options.UseNpgsql(connectionString));
                services.AddScoped<IVehicleRepository, VehicleRepository>();
                services.AddAutoMapper(typeof(Program).Assembly);
            }).Build();

        var app = host.Services.GetService<App>();
        app?.Run();
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    }

}
