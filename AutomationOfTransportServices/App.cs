using AutoMapper;
using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.ViewModels;
using AutomationOfTransportServices.Views;
using System.Windows;

namespace AutomationOfTransportServices;

public partial class App : Application
{
    private readonly MainView mainWindow;
    private readonly TransportServicesDbContext dbContext;
    private readonly IMapper mapper;
    private readonly IClientService clientService;
    private readonly IDriverService driverService;
    private readonly IServiceStringService serviceService;
    private readonly IServiceTypeService typeOfServiceService;
    private readonly IVehicleService vehicleService;

    public App(MainView mainWindow,
               TransportServicesDbContext dbContext,
               IMapper mapper,
               IClientService clientService,
               IDriverService driverService,
               IServiceStringService serviceService,
               IServiceTypeService typeOfServiceService,
               IVehicleService vehicleService)
    {
        this.mapper = mapper;
        this.mainWindow = mainWindow;
        this.dbContext = dbContext;
        this.clientService = clientService;
        this.driverService = driverService;
        this.serviceService = serviceService;
        this.typeOfServiceService = typeOfServiceService;
        this.vehicleService = vehicleService;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainViewModel = new MainViewModel(mainWindow,
                                               clientService,
                                               driverService,
                                               serviceService,
                                               typeOfServiceService,
                                               vehicleService);

        mainWindow.DataContext = mainViewModel;
        mainWindow.Closed += MainWindow_Closed;

        mainWindow.Show();
    }


    private void MainWindow_Closed(object sender, EventArgs e)
    {
        this.Shutdown();
    }
}
