using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace AutomationOfTransportServices.ViewModels;

public class ClientStatisticsViewModel : INotifyPropertyChanged
{
    private Window thisWindow;
    private IClientService clientService;

    private ObservableCollection<ClientStatisticModel> clientStatistics;
    public ObservableCollection<ClientStatisticModel> ClientStatistics
    {
        get => clientStatistics;
        set
        {
            clientStatistics = value;
            OnPropertyChanged(nameof(ClientStatistics));
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public ClientStatisticsViewModel(Window window, IClientService clientService)
    {
        thisWindow = window;
        this.clientService = clientService;

        LoadClientStatistics();
    }

    private void LoadClientStatistics()
    {
        var clients = clientService.GetAll();
        ClientStatistics = new ObservableCollection<ClientStatisticModel>();

        foreach (var client in clients)
        {
            var fullClient = clientService.GetById(client.Id);
            fullClient.Strings ??= new List<StringOfServiceModel>();

            ClientStatistics.Add(new ClientStatisticModel
            {
                Id = fullClient.Id,
                Name = fullClient.Name,
                NumberOfServices = fullClient.Strings.Count,
                TotalCost = fullClient.Strings.Sum(s => s.Cost)
            });
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
