using AutomationOfTransportServices.Collections;
using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class VehiclesViewModel: INotifyPropertyChanged
{
    private VehicleObservableCollection vehicles;
    private Command addVehicleCommand;
    private Command editVehicleCommand;
    private Command deleteVehicleCommand;
    private Command searchVehicleCommand;
    private Command refreshVehiclesCommand;
    private Window thisWindow;
    private IVehicleService vehicleService;
    private string searchText;
    private string executionTime;

    public ICommand AddVehicleCommand => addVehicleCommand;
    public ICommand EditVehicleCommand => editVehicleCommand;
    public ICommand DeleteVehicleCommand => deleteVehicleCommand;
    public ICommand SearchVehicleCommand => searchVehicleCommand;
    public ICommand RefreshVehiclesCommand => refreshVehiclesCommand;

    public ObservableCollection<VehicleModel> Vehicles => vehicles.Vehicles;

    public string ExecutionTime
    {
        get => executionTime + " ms";
        set
        {
            if (executionTime != value)
            {
                executionTime = value;
                OnPropertyChanged(nameof(ExecutionTime));
            }
        }
    }

    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged(nameof(SearchText));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public VehiclesViewModel(Window window, IVehicleService vehicleService)
    {
        this.vehicleService = vehicleService;
        vehicles = new VehicleObservableCollection(vehicleService);
        thisWindow = window;

        vehicles.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Vehicles));
            }
        };
        vehicles.ExecutionTimeUpdated += (newExecutionTime) =>
        {
            ExecutionTime = newExecutionTime.ToString();
        };
        ExecutionTime = vehicles.ExecutionTimeLastCommand.ToString();

        addVehicleCommand = new DelegateCommand(_ => AddVehicle());
        editVehicleCommand = new GenericCommand<VehicleModel>(x => EditVehicle(x), x => x != null);
        deleteVehicleCommand = new GenericCommand<VehicleModel>(x => DeleteVehicle(x), x => x != null);
        searchVehicleCommand = new DelegateCommand(_ => SearchVehicles());
        refreshVehiclesCommand = new DelegateCommand(_ => Refresh());
    }

    private void SearchVehicles()
    {
        vehicles.Filter(searchText);
    }

    private void Refresh()
    {
        SearchText = "";
        vehicles.Refresh();
    }

    private void AddVehicle()
    {
        var window = new AddEditVehicleView(thisWindow);
        var vehicleDetailsViewModel = new AddEditVehicleViewModel(window, vehicleService);
        window.DataContext = vehicleDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            var newVehicle = vehicleDetailsViewModel.Vehicle;
            if (newVehicle != null) vehicles.Add(newVehicle);
        }
    }

    private void EditVehicle(VehicleModel model)
    {
        if (model != null)
        {
            var window = new AddEditVehicleView(thisWindow);
            var vehicleDetailsViewModel = new AddEditVehicleViewModel(window, vehicleService, model.Clone() as VehicleModel);
            window.DataContext = vehicleDetailsViewModel;

            if (window.ShowDialog() == true)
            {
                var newVehicle = vehicleDetailsViewModel.Vehicle;
                if (newVehicle != null) vehicles.Update(newVehicle);
            }
        }
    }

    private void DeleteVehicle(VehicleModel model)
    {
        if (model == null) return;
        if(MessageBox.Show("Вы уверены, что хотите удалить этот автомобиль?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) vehicles.Delete(model.Id);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
