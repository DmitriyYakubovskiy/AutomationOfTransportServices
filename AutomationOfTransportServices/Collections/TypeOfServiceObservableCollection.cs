using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows;

namespace AutomationOfTransportServices.Collections;

public class TypeOfServiceObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<TypeOfServiceModel> typeOfServices;
    private IServiceTypeService serviceTypeService;

    public ObservableCollection<TypeOfServiceModel> TypeOfServices => typeOfServices;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public TypeOfServiceObservableCollection(IServiceTypeService typeOfServiceService)
    {
        this.serviceTypeService = typeOfServiceService;
        Init();
    }

    public void Add(TypeOfServiceModel typeOfService)
    {
        serviceTypeService.Create(typeOfService);
        Init();
    }

    public void Update(TypeOfServiceModel typeOfService)
    {
        serviceTypeService.Update(typeOfService);
        Init();
    }

    public TypeOfServiceModel GetTypeOfService(int id)
    {
        return serviceTypeService.GetById(id);
    }

    public void Delete(int id)
    {
        serviceTypeService.Delete(id); 
        Init();
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.typeOfServices = new ObservableCollection<TypeOfServiceModel>(serviceTypeService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { typeOfServices });

        stopwatch.Stop();
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
