using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AutomationOfTransportServices.ViewModels;

public class DriverStatisticsViewModel : INotifyPropertyChanged
{
    private Window thisWindow;
    private IDriverService driverService;

    private ObservableCollection<DriverStatisticModel> driverStatistics;
    public ObservableCollection<DriverStatisticModel> DriverStatistics
    {
        get => driverStatistics;
        set
        {
            driverStatistics = value;
            OnPropertyChanged(nameof(DriverStatistics));
        }
    }

    public DriverStatisticsViewModel(Window window, IDriverService driverService)
    {
        thisWindow = window;
        this.driverService = driverService;

        LoadDriverStatistics();
    }

    private void LoadDriverStatistics()
    {
        var drivers = driverService.GetAll();
        DriverStatistics = new ObservableCollection<DriverStatisticModel>();

        foreach (var driver in drivers)
        {
            var fullDriver = driverService.GetById(driver.Id);

            fullDriver.Strings ??= new List<StringOfServiceModel>();

            DriverStatistics.Add(new DriverStatisticModel
            {
                Id = fullDriver.Id,
                Name = fullDriver.Name,
                NumberOfServices = fullDriver.Strings.Count,
                TotalCost = fullDriver.Strings.Sum(s => s.Cost)
            });
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
