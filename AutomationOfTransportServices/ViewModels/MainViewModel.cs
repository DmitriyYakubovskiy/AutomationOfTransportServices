using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class MainViewModel
{
    private Command openClientsCommand;
    private Command openDriversCommand;
    private Command openStringsOfServiceCommand;
    private Command openTypesOfServicesCommand;
    private Command openVehiclesCommand;

    private Window thisWindow;
    private IClientService clientService;
    private IDriverService driverService;
    private IServiceStringService stringService;
    private IServiceTypeService typeOfServiceService;
    private IVehicleService vehicleService;

    public ICommand OpenClientsCommand => openClientsCommand;
    public ICommand OpenDriversCommand => openDriversCommand;
    public ICommand OpenStringsOfServiceCommand => openStringsOfServiceCommand;
    public ICommand OpenTypesOfServicesCommand => openTypesOfServicesCommand;
    public ICommand OpenVehiclesCommand => openVehiclesCommand;

    public MainViewModel(Window window, IClientService clientService, IDriverService driverService,
                         IServiceStringService serviceService, IServiceTypeService typeOfServiceService,
                         IVehicleService vehicleService)
    {
        this.thisWindow = window;
        this.clientService = clientService;
        this.driverService = driverService;
        this.stringService = serviceService;
        this.typeOfServiceService = typeOfServiceService;
        this.vehicleService = vehicleService;

        openClientsCommand = new DelegateCommand(_ => OpenClients());
        openDriversCommand = new DelegateCommand(_ => OpenDrivers());
        openStringsOfServiceCommand = new DelegateCommand(_ => OpenStringsOfServices());
        openTypesOfServicesCommand = new DelegateCommand(_ => OpenTypesOfServices());
        openVehiclesCommand = new DelegateCommand(_ => OpenVehicles());
    }

    private void OpenClients()
    {
        var window = new ClientsView(thisWindow);
        window.DataContext = new ClientsViewModel(window, clientService, stringService, driverService, typeOfServiceService, vehicleService);
        window.ShowDialog();
    }

    private void OpenDrivers()
    {
        var window = new DriversView(thisWindow);
        window.DataContext = new DriversViewModel(window, driverService);
        window.Show();
    }

    private void OpenStringsOfServices()
    {
        var window = new StringsOfServicesView(thisWindow);
        window.DataContext = new StringsOfServicesViewModel(window, stringService);
        window.Show();
    }

    private void OpenTypesOfServices()
    {
        var window = new TypesOfServicesView(thisWindow);
        window.DataContext = new TypesOfServicesViewModel(window, typeOfServiceService);
        window.Show();
    }

    private void OpenVehicles()
    {
        var window = new VehiclesView(thisWindow);
        window.DataContext = new VehiclesViewModel(window, vehicleService);
        window.Show();
    }
}
