using AutomationOfTransportServices.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using AutomationOfTransportServices.DataAccess.Helpers;
using AutomationOfTransportServices.Services;

namespace AutomationOfTransportServices.ViewModels;

public class AddEditVehicleViewModel : INotifyPropertyChanged
{
    private VehicleModel vehicle;
    private Window thisWindow;
    private IVehicleService vehicleService;

    public VehicleModel Vehicle
    {
        get => vehicle;
        set
        {
            vehicle = value;
            OnPropertyChanged(nameof(Vehicle));
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public AddEditVehicleViewModel(Window window, IVehicleService vehicleService, VehicleModel existingVehicle = null!)
    {
        thisWindow = window;
        Vehicle = existingVehicle ?? new VehicleModel();
        this.vehicleService = vehicleService;

        SaveCommand = new DelegateCommand(_ => Save());
        CancelCommand = new DelegateCommand(_ => Cancel());
    }

    private void Save()
    {
        if (String.IsNullOrEmpty(Vehicle.Name?.Trim()) || Vehicle?.Name?.Trim().Length != EntityRestrictions.vehicleNameLength) MessageBox.Show($"Неверная длина номера! Длина должна быть {EntityRestrictions.vehicleNameLength} символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (vehicleService.GetAll().Where(x => x.Name == Vehicle.Name).FirstOrDefault() != null) MessageBox.Show("Такой номер уже есть!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (Vehicle?.PriceOfKm<=0) MessageBox.Show("Цена не может быть ниже 1!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
