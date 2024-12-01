using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AutomationOfTransportServices.Collections;

public class ClientObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<ClientModel> clients;
    private IClientService clientService;

    public ObservableCollection<ClientModel> Clients => clients;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event Action<long> ExecutionTimeUpdated;
    public long ExecutionTimeLastCommand { get; private set; } = 0;

    public ClientObservableCollection(IClientService clientService)
    {
        this.clientService = clientService;
        Init();
    }

    public void Add(ClientModel client)
    {
        clientService.Create(client);
        Init();
    }

    public void Update(ClientModel client)
    {
        clientService.Update(client);
        Init();
    }

    public ClientModel GetClient(int id)
    {
        return clientService.GetById(id);
    }

    public void Delete(int id)
    {
        clientService.Delete(id); 
        Init();
    }

    public void Refresh()
    {
        Init();
    }

    public void Filter(string searchString)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var allClients = clientService.GetAll();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            clients = new ObservableCollection<ClientModel>(allClients);
        }
        else
        {
            var filteredClients = allClients.Where(x => x.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                 x.NumberOfTelephone.Contains(searchString)).ToList();

            clients = new ObservableCollection<ClientModel>(filteredClients);
        }
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { clients });
        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.clients = new ObservableCollection<ClientModel>(clientService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { clients });

        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
