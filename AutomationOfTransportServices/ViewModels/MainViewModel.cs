using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class MainViewModel
{
    private Command openClientsCommand;
    private Window thisWindow;
    private IClientService clientService;

    public ICommand OpenClientsCommand => openClientsCommand;

    public MainViewModel(Window window, IClientService clientService)
    {
        this.thisWindow = window;
        this.clientService = clientService;
        openClientsCommand = new DelegateCommand(_ => OpenClients());
    }

    private void OpenClients()
    {
        var window = new ClientsView(thisWindow);
        window.DataContext = new ClientsViewModel(window,clientService);
        window.Show();
    }
}
