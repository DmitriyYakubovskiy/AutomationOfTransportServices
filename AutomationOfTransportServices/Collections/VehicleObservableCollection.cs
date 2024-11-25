using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;

namespace AutomationOfTransportServices.Collections;

public class VehicleObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<VehicleModel> vehicles;
    private IVehicleService vehicleService;

    public ObservableCollection<VehicleModel> Vehicles => vehicles;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

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

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.vehicles = new ObservableCollection<VehicleModel>(vehicleService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { vehicles });

        stopwatch.Stop();
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
