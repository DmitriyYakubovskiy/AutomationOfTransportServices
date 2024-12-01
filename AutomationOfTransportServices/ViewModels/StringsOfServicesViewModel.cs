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

public class StringsOfServicesViewModel : INotifyPropertyChanged
{
    private StringOfServiceObservableCollection strings;
    private Command addStringCommand;
    private Command editStringCommand;
    private Command deleteStringCommand;
    private Command searchStringCommand;
    private Command refreshStringsCommand;
    private Window thisWindow;
    private IClientService clientService;
    private IServiceStringService stringService;
    private IDriverService driverService;
    private IServiceTypeService typeService;
    private IVehicleService vehicleService;
    private string searchText;
    private string executionTime;

    public ICommand AddStringCommand => addStringCommand;
    public ICommand EditStringCommand => editStringCommand;
    public ICommand DeleteStringCommand => deleteStringCommand;
    public ICommand SearchStringCommand => searchStringCommand;
    public ICommand RefreshStringsCommand => refreshStringsCommand;

    public ObservableCollection<StringOfServiceModel> Strings => strings.Strings;

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

    public StringsOfServicesViewModel(Window window, IClientService clientService, IServiceStringService stringService, IDriverService driverService, IServiceTypeService typeService, IVehicleService vehicleService)
    {
        this.clientService = clientService;
        this.stringService = stringService;
        this.driverService = driverService;
        this.typeService = typeService;
        this.vehicleService = vehicleService;
        strings = new StringOfServiceObservableCollection(stringService);
        thisWindow = window;

        strings.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Strings));
            }
        };
        strings.ExecutionTimeUpdated += (newExecutionTime) =>
        {
            ExecutionTime = newExecutionTime.ToString();
        };
        ExecutionTime = strings.ExecutionTimeLastCommand.ToString();

        addStringCommand = new DelegateCommand(_ => AddString());
        editStringCommand = new GenericCommand<StringOfServiceModel>(x => EditString(x), x => x != null);
        deleteStringCommand = new GenericCommand<StringOfServiceModel>(x => DeleteString(x), x => x != null);
        searchStringCommand = new DelegateCommand(_ => SearchStrings());
        refreshStringsCommand = new DelegateCommand(_ => Refresh());
    }

    private void SearchStrings()
    {
        strings.Filter(searchText);
    }

    private void Refresh()
    {
        SearchText = "";
        strings.Refresh();
    }

    private void AddString()
    {
        var window = new AddEditStringOfServiceView(thisWindow);
        var stringDetailsViewModel = new AddEditStringOfServiceViewModel(window, clientService, stringService, driverService, typeService, vehicleService);
        window.DataContext = stringDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            var newString = stringDetailsViewModel.StringOfService;
            if (newString != null) strings.Add(newString);
        }
    }

    private void EditString(StringOfServiceModel model)
    {
        if (model != null)
        {
            var window = new AddEditStringOfServiceView(thisWindow);
            var stringDetailsViewModel = new AddEditStringOfServiceViewModel(window, clientService, stringService, driverService, typeService, vehicleService, model.Client, model.Clone() as StringOfServiceModel);
            window.DataContext = stringDetailsViewModel;

            if (window.ShowDialog() == true)
            {
                var newString = stringDetailsViewModel.StringOfService;
                if (newString != null) strings.Update(newString);
            }
        }
    }

    private void DeleteString(StringOfServiceModel model)
    {
        if (model == null) return;
        if (MessageBox.Show("Вы уверены, что хотите удалить эту услугу?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) strings.Delete(model.Id);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
