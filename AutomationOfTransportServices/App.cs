using AutoMapper;
using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.ViewModels;
using AutomationOfTransportServices.Views;
using System.Windows;

namespace AutomationOfTransportServices;

public partial class App : Application
{
    readonly private MainView mainWindow;
    readonly private TransportServicesDbContext dbContext;
    readonly private IMapper mapper;
    readonly private IClientService clientService;


    public App(MainView mainWindow, TransportServicesDbContext dbContext, IMapper mapper,IClientService clientService)
    {
        this.mapper = mapper;
        this.mainWindow = mainWindow;
        this.dbContext = dbContext;
        this.clientService = clientService;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        mainWindow.DataContext = new MainViewModel(mainWindow, clientService);
        mainWindow.Show();
        base.OnStartup(e);
    }
}
