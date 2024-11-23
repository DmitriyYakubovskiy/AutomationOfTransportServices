using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AutomationOfTransportServices.ViewModels;

public class ClientsViewModel : INotifyPropertyChanged
{
    private readonly IClientService clientService;
    private ObservableCollection<ClientModel> clients;

    public ObservableCollection<ClientModel> Clients
    {
        get { return clients; }
        set
        {
            clients = value;
            OnPropertyChanged(nameof(Clients));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public ClientsViewModel(Window window, IClientService clientService)
    {
        this.clientService = clientService;
        Clients = new ObservableCollection<ClientModel>();
        LoadClients();
    }

    private void LoadClients()
    {
        var clientsArray = clientService.GetAll();
        Clients.Clear();
        foreach (var client in clientsArray)
        {
            Clients.Add(client);
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}