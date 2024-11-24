using AutomationOfTransportServices.Collections;
using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
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
    private readonly IClientService clientService;

    public ICommand AddClientCommand => addClientCommand;
    public IReadOnlyCollection<ClientModel> Clients => clients.Clients;
    public event PropertyChangedEventHandler PropertyChanged;

    public ClientsViewModel(Window window, IClientService clientService)
    {
        this.clientService = clientService;
        clients = new ClientObservableCollection(clientService);
        clients.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Clients));
            }
        };
        addClientCommand = new DelegateCommand(_ => AddClient());
    }

    private void AddClient()
    {
        clients.Add(new ClientModel { Name = "aaaa", NumberOfTelephone = "11111" });
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}