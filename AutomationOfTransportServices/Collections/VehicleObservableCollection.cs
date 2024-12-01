using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AutomationOfTransportServices.Collections;

public class VehicleObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<VehicleModel> vehicles;
    private IVehicleService vehicleService;

    public ObservableCollection<VehicleModel> Vehicles => vehicles;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event Action<long> ExecutionTimeUpdated;
    public long ExecutionTimeLastCommand { get; private set; } = 0;

    public VehicleObservableCollection(IVehicleService vehicleService)
    {
        this.vehicleService = vehicleService;
        Init();
    }

    public void Add(VehicleModel vehicle)
    {
        vehicleService.Create(vehicle);
        Init();
    }

    public void Update(VehicleModel vehicle)
    {
        vehicleService.Update(vehicle);
        Init();
    }

    public VehicleModel GetVehicle(int id)
    {
        return vehicleService.GetById(id);
    }

    public void Delete(int id)
    {
        vehicleService.Delete(id); 
        Init();
    }

    public void Refresh()
    {
        Init();
    }

    public void Filter(string searchString)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var all = vehicleService.GetAll();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            vehicles = new ObservableCollection<VehicleModel>(all);
        }
        else
        {
            var filtered = all.Where(x => x.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            vehicles = new ObservableCollection<VehicleModel>(filtered);
        }
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { vehicles });
        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.vehicles = new ObservableCollection<VehicleModel>(vehicleService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { vehicles });

        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
