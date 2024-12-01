using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AutomationOfTransportServices.ViewModels;

public class VehicleStatisticsViewModel : INotifyPropertyChanged
{
    private Window thisWindow;
    private IVehicleService vehicleService;

    private ObservableCollection<VehicleStatisticModel> vehicleStatistics;
    public ObservableCollection<VehicleStatisticModel> VehicleStatistics
    {
        get => vehicleStatistics;
        set
        {
            vehicleStatistics = value;
            OnPropertyChanged(nameof(VehicleStatistics));
        }
    }

    public VehicleStatisticsViewModel(Window window, IVehicleService vehicleService)
    {
        thisWindow = window;
        this.vehicleService = vehicleService;

        LoadVehicleStatistics();
    }

    private void LoadVehicleStatistics()
    {
        var vehicles = vehicleService.GetAll();
        VehicleStatistics = new ObservableCollection<VehicleStatisticModel>();

        foreach (var vehicle in vehicles)
        {
            var fullVehicle = vehicleService.GetById(vehicle.Id);

            fullVehicle.Strings ??= new List<StringOfServiceModel>();

            VehicleStatistics.Add(new VehicleStatisticModel
            {
                Id = fullVehicle.Id,
                Name = fullVehicle.Name,
                NumberOfServices = fullVehicle.Strings.Count,
                TotalCost = fullVehicle.Strings.Sum(s => s.Cost)
            });
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}