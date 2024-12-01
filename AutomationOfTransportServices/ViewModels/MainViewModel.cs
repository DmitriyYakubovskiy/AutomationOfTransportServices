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
    private Command openStringsOfServicesCommand;
    private Command openTypesOfServicesCommand;
    private Command openVehiclesCommand;
    private Command showClientStatisticsCommand;
    private Command showVehicleStatisticsCommand;
    private Command showServiceTypeStatisticsCommand;
    private Command showDriverStatisticsCommand;

    private Window thisWindow;
    private IClientService clientService;
    private IDriverService driverService;
    private IServiceStringService stringService;
    private IServiceTypeService typeOfServiceService;
    private IVehicleService vehicleService;

    public ICommand OpenClientsCommand => openClientsCommand;
    public ICommand OpenDriversCommand => openDriversCommand;
    public ICommand OpenStringsOfServicesCommand => openStringsOfServicesCommand;
    public ICommand OpenTypesOfServicesCommand => openTypesOfServicesCommand;
    public ICommand OpenVehiclesCommand => openVehiclesCommand;
    public ICommand ShowClientStatisticsCommand => showClientStatisticsCommand;
    public ICommand ShowVehicleStatisticsCommand => showVehicleStatisticsCommand;
    public ICommand ShowServiceTypeStatisticsCommand => showServiceTypeStatisticsCommand;
    public ICommand ShowDriverStatisticsCommand => showDriverStatisticsCommand;

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
        openStringsOfServicesCommand = new DelegateCommand(_ => OpenStringsOfServices());
        openTypesOfServicesCommand = new DelegateCommand(_ => OpenTypesOfServices());
        openVehiclesCommand = new DelegateCommand(_ => OpenVehicles());
        showClientStatisticsCommand = new DelegateCommand(_ => ShowClientStatistics());
        showVehicleStatisticsCommand = new DelegateCommand(_ => ShowVehicleStatistics());
        showServiceTypeStatisticsCommand = new DelegateCommand(_ => ShowServiceTypeStatistics());
        showDriverStatisticsCommand = new DelegateCommand(_ => ShowDriverStatistics());
    }

    private void OpenClients()
    {
        var window = new ClientsView(thisWindow);
        window.DataContext = new ClientsViewModel(window, clientService, stringService, driverService, typeOfServiceService, vehicleService);
        window.Show();
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
        window.DataContext = new StringsOfServicesViewModel(window, clientService, stringService, driverService, typeOfServiceService, vehicleService);
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

    private void ShowClientStatistics()
    {
        var statisticsWindow = new ClientStatisticsView(thisWindow);
        statisticsWindow.DataContext = new ClientStatisticsViewModel(statisticsWindow, clientService); 
        statisticsWindow.Show();
    }

    private void ShowVehicleStatistics()
    {
        var statisticsWindow = new VehicleStatisticsView(thisWindow);
        statisticsWindow.DataContext = new VehicleStatisticsViewModel(statisticsWindow, vehicleService);
        statisticsWindow.Show();
    }

    private void ShowServiceTypeStatistics()
    {
        var statisticsWindow = new ServiceTypeStatisticsView(thisWindow);
        statisticsWindow.DataContext = new ServiceTypeStatisticsViewModel(statisticsWindow, typeOfServiceService);
        statisticsWindow.Show();
    }

    private void ShowDriverStatistics()
    {
        var statisticsWindow = new DriverStatisticsView(thisWindow);
        statisticsWindow.DataContext = new DriverStatisticsViewModel(statisticsWindow, driverService);
        statisticsWindow.Show();
    }
}

