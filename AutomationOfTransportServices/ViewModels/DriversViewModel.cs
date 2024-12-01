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

public class DriversViewModel : INotifyPropertyChanged
{
    private DriverObservableCollection drivers;
    private Command addDriverCommand;
    private Command editDriverCommand;
    private Command deleteDriverCommand;
    private Command searchDriverCommand;
    private Command refreshDriversCommand;
    private Window thisWindow;
    private IDriverService driverService;
    private string searchText;
    private string executionTime;

    public ICommand AddDriverCommand => addDriverCommand;
    public ICommand EditDriverCommand => editDriverCommand;
    public ICommand DeleteDriverCommand => deleteDriverCommand;
    public ICommand SearchDriverCommand => searchDriverCommand;
    public ICommand RefreshDriversCommand => refreshDriversCommand;

    public ObservableCollection<DriverModel> Drivers => drivers.Drivers;

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

    public DriversViewModel(Window window, IDriverService driverService)
    {
        this.driverService = driverService;
        drivers = new DriverObservableCollection(driverService);
        thisWindow = window;

        drivers.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Drivers));
            }
        };
        drivers.ExecutionTimeUpdated += (newExecutionTime) =>
        {
            ExecutionTime = newExecutionTime.ToString();
        };
        ExecutionTime = drivers.ExecutionTimeLastCommand.ToString();

        addDriverCommand = new DelegateCommand(_ => AddDriver());
        editDriverCommand = new GenericCommand<DriverModel>(x => EditDriver(x), x => x != null);
        deleteDriverCommand = new GenericCommand<DriverModel>(x => DeleteDriver(x), x => x != null);
        searchDriverCommand = new DelegateCommand(_ => SearchDrivers());
        refreshDriversCommand = new DelegateCommand(_ => Refresh());
    }

    private void SearchDrivers()
    {
        SearchText = "";
        drivers.Filter(searchText);
    }

    private void Refresh()
    {
        drivers.Refresh();
    }

    private void AddDriver()
    {
        var window = new AddEditDriverView(thisWindow);
        var driverDetailsViewModel = new AddEditDriverViewModel(window);
        window.DataContext = driverDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            var newDriver = driverDetailsViewModel.Driver;
            if (newDriver != null) drivers.Add(newDriver);
        }
    }

    private void EditDriver(DriverModel model)
    {
        if (model != null)
        {
            var window = new AddEditDriverView(thisWindow);
            var driverDetailsViewModel = new AddEditDriverViewModel(window, model.Clone() as DriverModel);
            window.DataContext = driverDetailsViewModel;

            if (window.ShowDialog() == true)
            {
                var newDriver = driverDetailsViewModel.Driver;
                if (newDriver != null) drivers.Update(newDriver);
            }
        }
    }

    private void DeleteDriver(DriverModel model)
    {
        if (model == null) return;
        if (MessageBox.Show("Вы уверены, что хотите удалить этого водителя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) drivers.Delete(model.Id);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
