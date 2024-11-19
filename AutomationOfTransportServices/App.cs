using AutoMapper;
using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.Views;
using System.Windows;

namespace AutomationOfTransportServices;

public partial class App : Application
{
    readonly MainWindow mainWindow;
    readonly TransportServicesDbContext dbContext;
    readonly IMapper mapper;

    public App(MainWindow mainWindow, TransportServicesDbContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.mainWindow = mainWindow;
        this.dbContext = dbContext;
        //dbContext.Vehicles.Add(new VehicleEntity() { Name = "B111BF", PriceOfKm = 12 });
        //dbContext.SaveChanges();
        MessageBox.Show(dbContext.Vehicles.First().Name);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        mainWindow.Show();
        base.OnStartup(e);
    }
}
