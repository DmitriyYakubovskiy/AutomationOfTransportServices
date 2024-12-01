using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AutomationOfTransportServices.Collections;

public class DriverObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<DriverModel> drivers;
    private IDriverService driverService;

    public ObservableCollection<DriverModel> Drivers => drivers;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event Action<long> ExecutionTimeUpdated;
    public long ExecutionTimeLastCommand { get; private set; } = 0;

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

    public void Refresh()
    {
        Init();
    }

    public void Filter(string searchString)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var allDrivers = driverService.GetAll();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            drivers = new ObservableCollection<DriverModel>(allDrivers);
        }
        else
        {
            var filteredDrivers = allDrivers
                .Where(x => x.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            drivers = new ObservableCollection<DriverModel>(filteredDrivers);
        }
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { drivers });
        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.drivers = new ObservableCollection<DriverModel>(driverService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { drivers });

        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
