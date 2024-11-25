using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;

namespace AutomationOfTransportServices.Collections;

public class StringOfServiceObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<StringOfServiceModel> stringOfServices;
    private IServiceStringService stringOfServiceService;

    public ObservableCollection<StringOfServiceModel> StringOfServices => stringOfServices;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public StringOfServiceObservableCollection(IServiceStringService stringOfServiceService)
    {
        this.stringOfServiceService = stringOfServiceService;
        Init();
    }

    public void Add(StringOfServiceModel stringOfService)
    {
        stringOfServiceService.Create(stringOfService);
        Init();
    }

    public void Update(StringOfServiceModel stringOfService)
    {
        stringOfServiceService.Update(stringOfService);
        Init();
    }

    public StringOfServiceModel GetStringOfService(int id)
    {
        return stringOfServiceService.GetById(id);
    }

    public void Delete(int id)
    {
        stringOfServiceService.Delete(id); 
        Init();
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.stringOfServices = new ObservableCollection<StringOfServiceModel>(stringOfServiceService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { stringOfServices });

        stopwatch.Stop();
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
