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
    private readonly IClientService clientService;
    private readonly IDriverService driverService;
    private readonly IServiceStringService serviceService;
    private readonly IServiceTypeService typeOfServiceService;
    private readonly IVehicleService vehicleService;

    public App(MainView mainWindow,
               IClientService clientService,
               IDriverService driverService,
               IServiceStringService serviceService,
               IServiceTypeService typeOfServiceService,
               IVehicleService vehicleService)
    {
        this.mainWindow = mainWindow;
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
