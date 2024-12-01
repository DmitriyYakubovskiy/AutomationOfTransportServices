using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AutomationOfTransportServices.ViewModels;

public class ServiceTypeStatisticsViewModel : INotifyPropertyChanged
{
    private Window thisWindow;
    private IServiceTypeService serviceTypeService;

    private ObservableCollection<ServiceTypeStatisticModel> serviceTypeStatistics;
    public ObservableCollection<ServiceTypeStatisticModel> ServiceTypeStatistics
    {
        get => serviceTypeStatistics;
        set
        {
            serviceTypeStatistics = value;
            OnPropertyChanged(nameof(ServiceTypeStatistics));
        }
    }

    public ServiceTypeStatisticsViewModel(Window window, IServiceTypeService serviceTypeService)
    {
        thisWindow = window;
        this.serviceTypeService = serviceTypeService;

        LoadServiceTypeStatistics();
    }

    private void LoadServiceTypeStatistics()
    {
        var serviceTypes = serviceTypeService.GetAll();
        ServiceTypeStatistics = new ObservableCollection<ServiceTypeStatisticModel>();

        foreach (var serviceType in serviceTypes)
        {
            var fullServiceType = serviceTypeService.GetById(serviceType.Id);

            fullServiceType.Strings ??= new List<StringOfServiceModel>();

            ServiceTypeStatistics.Add(new ServiceTypeStatisticModel
            {
                Id = fullServiceType.Id,
                Name = fullServiceType.Name,
                NumberOfServices = fullServiceType.Strings.Count,
                TotalCost = fullServiceType.Strings.Sum(s => s.Cost)
            });
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}