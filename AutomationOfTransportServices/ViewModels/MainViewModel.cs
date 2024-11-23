using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class MainViewModel
{
    public ICommand OpenClientsCommand => openClientsCommand;
    private Command openClientsCommand;
    private Window thisWindow;
    private IClientService clientService;

    public MainViewModel(Window window, IClientService clientService)
    {
        this.thisWindow = window;

        openClientsCommand = new DelegateCommand(_ => OpenClients());
        this.clientService = clientService;
    }

    private void OpenClients()
    {
        var window = new ClientsView(thisWindow);
        window.DataContext = new ClientsViewModel(window,clientService);
    }
}
