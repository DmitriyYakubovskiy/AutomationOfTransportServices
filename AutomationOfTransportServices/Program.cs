using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutomationOfTransportServices.Views;
using AutomationOfTransportServices.Services;
using System.Windows;

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
                services.AddSingleton<MainView>();
                services.AddDbContext<TransportServicesDbContext>(options => options.UseNpgsql(connectionString));
                services.AddScoped<IVehicleRepository, VehicleRepository>();
                services.AddScoped<IClientRepository, ClientRepository>();
                services.AddScoped<IDriverRepository, DriverRepository>();
                services.AddScoped<ITypeOfServiceRepository, TypeOfServiceRepository>();
                services.AddScoped<IStringOfServiceRepository, StringOfServiceRepository>();

                services.AddScoped<IVehicleService, VehicleService>();
                services.AddScoped<IClientService, ClientService>();
                services.AddScoped<IDriverService, DriverService>();
                services.AddScoped<ITypeOfServiceService, TypeOfServiceService>();
                services.AddScoped<IStringOfServiceService, StringOfServiceService>();

                services.AddAutoMapper(typeof(Program).Assembly);
            }).Build();

        var app = host.Services.GetService<App>();
        app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
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
