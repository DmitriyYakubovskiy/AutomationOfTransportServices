using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AutomationOfTransportServices.Collections;

public class TypeOfServiceObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<TypeOfServiceModel> types;
    private IServiceTypeService typeService;

    public ObservableCollection<TypeOfServiceModel> Types => types;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event Action<long> ExecutionTimeUpdated;
    public long ExecutionTimeLastCommand { get; private set; } = 0;

    public TypeOfServiceObservableCollection(IServiceTypeService typeOfServiceService)
    {
        this.typeService = typeOfServiceService;
        Init();
    }

    public void Add(TypeOfServiceModel typeOfService)
    {
        typeService.Create(typeOfService);
        Init();
    }

    public void Update(TypeOfServiceModel typeOfService)
    {
        typeService.Update(typeOfService);
        Init();
    }

    public TypeOfServiceModel GetTypeOfService(int id)
    {
        return typeService.GetById(id);
    }

    public void Delete(int id)
    {
        typeService.Delete(id); 
        Init();
    }

    public void Refresh()
    {
        Init();
    }

    public void Filter(string searchString)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var all = typeService.GetAll();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            types = new ObservableCollection<TypeOfServiceModel>(all);
        }
        else
        {
            var filtered = all
                .Where(x => x.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            types = new ObservableCollection<TypeOfServiceModel>(filtered);
        }
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { types });
        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.types = new ObservableCollection<TypeOfServiceModel>(typeService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { types });

        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
