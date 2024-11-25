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
    private Window thisWindow;
    private string searchText;

    public ICommand AddClientCommand => addClientCommand;
    public ICommand EditClientCommand => editClientCommand;
    public ICommand DeleteClientCommand => deleteClientCommand;
    public ICommand SearchClientCommand => searchClientCommand;

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

    public ClientsViewModel(Window window, IClientService clientService)
    {
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
        searchClientCommand = new DelegateCommand(_ => SearchClients()); 
    }

    private void SearchClients()
    {
        clients.Filter(searchText);
    }

    private void AddClient()
    {
        var window = new ClientDetailsView(thisWindow);
        var clientDetailsViewModel = new ClientDetailsViewModel(window);
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
            var window = new ClientDetailsView(thisWindow);
            var clientDetailsViewModel = new ClientDetailsViewModel(window, client.Clone() as ClientModel);
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

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}