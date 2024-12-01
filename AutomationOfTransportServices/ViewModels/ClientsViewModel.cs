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

public class ClientsViewModel : INotifyPropertyChanged
{
    private ClientObservableCollection clients;
    private Command addClientCommand;
    private Command editClientCommand;
    private Command deleteClientCommand;
    private Command searchClientCommand;
    private Command showDocumentCommand;
    private Command refreshClientsCommand;
    private Window thisWindow;
    private IClientService clientService;
    private IServiceStringService stringService;
    private IDriverService driverService;
    private IServiceTypeService typeService;
    private IVehicleService vehicleService;
    private string searchText;

    public ICommand AddClientCommand => addClientCommand;
    public ICommand EditClientCommand => editClientCommand;
    public ICommand DeleteClientCommand => deleteClientCommand;
    public ICommand SearchClientCommand => searchClientCommand;
    public ICommand ShowDocumentCommand => showDocumentCommand;
    public ICommand RefreshClientsCommand => refreshClientsCommand; 

    public ObservableCollection<ClientModel> Clients => clients.Clients;

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

    public ClientsViewModel(Window window, IClientService clientService, IServiceStringService stringService, IDriverService driverService, IServiceTypeService typeService, IVehicleService vehicleService)
    {
        this.clientService = clientService;
        this.stringService = stringService;
        this.driverService = driverService;
        this.typeService = typeService;
        this.vehicleService = vehicleService;
        clients = new ClientObservableCollection(clientService);
        thisWindow = window;

        clients.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Clients));
            }
        };

        addClientCommand = new DelegateCommand(_ => AddClient());
        editClientCommand = new GenericCommand<ClientModel>(client => EditClient(client), client => client != null);
        deleteClientCommand = new GenericCommand<ClientModel>(client => DeleteClient(client), client => client != null);
        showDocumentCommand = new GenericCommand<ClientModel>(client => ShowDocument(client), client => client != null);
        searchClientCommand = new DelegateCommand(_ => SearchClients());
        refreshClientsCommand = new DelegateCommand(_ => Refresh());
    }

    private void SearchClients()
    {
        clients.Filter(searchText);
    }

    private void Refresh()
    {
        clients.Refresh();
    }

    private void AddClient()
    {
        var window = new AddEditClientView(thisWindow);
        var clientDetailsViewModel = new AddEditClientViewModel(window);
        window.DataContext = clientDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            var newClient = clientDetailsViewModel.Client;
            if (newClient != null)
            {
                clients.Add(newClient);
            }
        }
    }

    private void EditClient(ClientModel client)
    {
        if (client != null)
        {
            var window = new AddEditClientView(thisWindow);
            var clientDetailsViewModel = new AddEditClientViewModel(window, client.Clone() as ClientModel);
            window.DataContext = clientDetailsViewModel;

            if (window.ShowDialog() == true)
            {
                var newClient = clientDetailsViewModel.Client;
                if (newClient != null)
                {
                    clients.Update(newClient);
                }
            }
        }
    }

    private void DeleteClient(ClientModel client)
    {
        if (client == null) return;
        clients.Delete(client.Id);
    }

    private void ShowDocument(ClientModel client)
    {
        Window window = new DocumentView(thisWindow);
        DocumentViewModel viewModel = new DocumentViewModel(window, clientService, stringService, driverService, typeService, vehicleService, client.Id);
        window.DataContext = viewModel;
        window.ShowDialog();
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}