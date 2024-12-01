using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class DocumentViewModel : INotifyPropertyChanged
{
    private ClientModel client;
    private Window thisWindow;
    private IClientService clientService;
    private IServiceStringService stringService;
    private IDriverService driverService;
    private IServiceTypeService typeService;
    private IVehicleService vehicleService;

    private Command refreshServicesCommand;
    private Command addStringOfServiceCommand;
    private Command deleteStringOfServiceCommand;
    private Command editStringOfServiceCommand;

    public ICommand RefreshServicesCommand=>refreshServicesCommand;
    public ICommand AddStringOfServiceCommand => addStringOfServiceCommand;
    public ICommand DeleteStringOfServiceCommand =>deleteStringOfServiceCommand;
    public ICommand EditStringOfServiceCommand => editStringOfServiceCommand;

    public ClientModel Client
    {
        get { return client; }
        set
        {
            client = value;
            OnPropertyChanged(nameof(Client));
            OnPropertyChanged(nameof(Strings));
        }
    }

    public List<StringOfServiceModel> Strings => Client.Strings;

    public event PropertyChangedEventHandler PropertyChanged;

    public DocumentViewModel(Window window, IClientService clientService, IServiceStringService stringService, IDriverService driverService, IServiceTypeService typeService, IVehicleService vehicleService, int clientId)
    {
        Client = clientService.GetById(clientId);
        if (client == null) window.Close();
        thisWindow = window;
        this.clientService = clientService;
        this.stringService = stringService;
        this.driverService = driverService;
        this.typeService = typeService;
        this.vehicleService = vehicleService;
        refreshServicesCommand = new DelegateCommand(_ => Refresh());
        addStringOfServiceCommand= new DelegateCommand(_ => AddStringOfService());
        deleteStringOfServiceCommand = new GenericCommand<StringOfServiceModel>(model => DeleteStringOfService(model), model => model != null);
        editStringOfServiceCommand = new GenericCommand<StringOfServiceModel>(model => EditStringOfService(model), model => model != null);
    }

    private void AddStringOfService()
    {
        var window = new AddEditStringOfServiceView(thisWindow);
        var viewModel=new AddEditStringOfServiceViewModel(window, clientService, stringService, driverService, typeService, vehicleService, client);  
        window.DataContext = viewModel;
        if (window.ShowDialog() == true)
        {
            if (viewModel.StringOfService != null) stringService.Create(viewModel.StringOfService);
            Refresh();
        }
    }

    private void Refresh()
    {
        Client=clientService.GetById(client.Id);
    }

    private void DeleteStringOfService(StringOfServiceModel model)
    {
        if (model == null) return;
        stringService.Delete(model.Id);
        Refresh();
    }

    private void EditStringOfService(StringOfServiceModel model)
    {
        if (model == null) return ;
        var window = new AddEditStringOfServiceView(thisWindow);
        var stringDetailsViewModel = new AddEditStringOfServiceViewModel(window, clientService, stringService, driverService, typeService, vehicleService, model.Client.Clone() as ClientModel, model.Clone() as StringOfServiceModel);
        window.DataContext = stringDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            if (stringDetailsViewModel.StringOfService != null) stringService.Update(stringDetailsViewModel.StringOfService);
            Refresh();
        }
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}