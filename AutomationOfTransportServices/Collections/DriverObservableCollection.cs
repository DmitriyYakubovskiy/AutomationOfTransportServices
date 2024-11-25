using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;

namespace AutomationOfTransportServices.Collections;

public class DriverObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<DriverModel> drivers;
    private IDriverService driverService;

    public ObservableCollection<DriverModel> Drivers => drivers;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public DriverObservableCollection(IDriverService driverService)
    {
        this.driverService = driverService;
        Init();
    }

    public void Add(DriverModel driver)
    {
        driverService.Create(driver);
        Init();
    }

    public void Update(DriverModel driver)
    {
        driverService.Update(driver);
        Init();
    }

    public DriverModel GetDriver(int id)
    {
        return driverService.GetById(id);
    }

    public void Delete(int id)
    {
        driverService.Delete(id); 
        Init();
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.drivers = new ObservableCollection<DriverModel>(driverService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { drivers });

        stopwatch.Stop();
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
