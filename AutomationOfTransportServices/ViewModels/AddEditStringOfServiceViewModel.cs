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
    public List<TypeOfServiceModel> Types { get; set; }
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
        Types = typeService.GetAll().ToList();
        Drivers = driverService.GetAll().ToList();
        Vehicles = vehicleService.GetAll().ToList();
        Clients = clientService.GetAll().ToList();

        StringOfService.DateTime=DateTime.Now;

        if (existingString != null)
        {
            if (existingString.Driver != null) StringOfService.Driver = Drivers.Where(x => x.Id == existingString.Driver.Id).FirstOrDefault()!;
            if (existingString.TypeOfService != null) StringOfService.TypeOfService = Types.Where(x => x.Id == existingString.TypeOfService.Id).FirstOrDefault()!;
            if (existingString.Vehicle!=null) StringOfService.Vehicle = Vehicles.Where(x => x.Id == existingString.Vehicle.Id).FirstOrDefault()!;
        }

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
        if (StringOfService.Client == null) MessageBox.Show("Выберите клиента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (StringOfService.Driver == null) MessageBox.Show("Выберите водителя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (StringOfService.TypeOfService == null) MessageBox.Show("Выберите тип услуги!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (StringOfService.Vehicle == null) MessageBox.Show("Выберите автомобиль!","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (StringOfService?.Distance <= 0) MessageBox.Show("Расстояние не может быть ниже 1!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else thisWindow.DialogResult = true;
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