using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class AddEditStringOfServiceViewModel : INotifyPropertyChanged
{
    private StringOfServiceModel stringOfService;
    private Window thisWindow;
    private IClientService clientService;
    private IServiceStringService stringService;
    private IDriverService driverService;
    private IServiceTypeService typeService;
    private IVehicleService vehicleService;

    public StringOfServiceModel StringOfService
    {
        get => stringOfService;
        set
        {
            stringOfService = value;
            OnPropertyChanged(nameof(StringOfService));
        }
    }

    public List<ClientModel> Clients { get; set; }
    public List<VehicleModel> Vehicles { get; set; }
    public List<TypeOfServiceModel> ServiceTypes { get; set; }
    public List<DriverModel> Drivers { get; set; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    private bool isClientEditable;
    public bool IsClientEditable
    {
        get => isClientEditable;
        set
        {
            isClientEditable = value;
            OnPropertyChanged(nameof(IsClientEditable));
        }
    }

    public AddEditStringOfServiceViewModel(Window window, IClientService clientService, IServiceStringService stringService, IDriverService driverService, IServiceTypeService typeService, IVehicleService vehicleService, ClientModel client = null!, StringOfServiceModel existingString = null!)
    {
        this.clientService = clientService;
        this.stringService = stringService;
        this.driverService = driverService;
        this.typeService = typeService;
        this.vehicleService = vehicleService;

        thisWindow = window;
        StringOfService = existingString ?? new StringOfServiceModel();

        this.stringService=stringService;
        ServiceTypes = typeService.GetAll().ToList();
        Drivers = driverService.GetAll().ToList();
        Vehicles = vehicleService.GetAll().ToList();
        Clients = clientService.GetAll().ToList();

        if (client != null)
        {
            StringOfService.Client = Clients.Where(x=>x.Id== client.Id).FirstOrDefault()!;
            IsClientEditable = false;
        }
        else
        {
            IsClientEditable = true; 
        }

        SaveCommand = new DelegateCommand(_ => Save());
        CancelCommand = new DelegateCommand(_ => Cancel());
    }

    private void Save()
    {
        if (stringOfService != null)
        {
            int N = stringService.GetAll().Where(x => x.ClientId == stringOfService.ClientId).Max(x=>x.Number)+1;
            stringOfService.Number = N;
            stringService.Create(StringOfService);
        }
        thisWindow.DialogResult = true;
    }

    private void Cancel()
    {
        thisWindow.DialogResult = false;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}