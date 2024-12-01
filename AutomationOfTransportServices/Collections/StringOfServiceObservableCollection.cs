using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;

namespace AutomationOfTransportServices.Collections;

public class StringOfServiceObservableCollection : INotifyCollectionChanged
{
    private ObservableCollection<StringOfServiceModel> strings;
    private IServiceStringService stringService;

    public ObservableCollection<StringOfServiceModel> Strings => strings;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event Action<long> ExecutionTimeUpdated;
    public long ExecutionTimeLastCommand { get; private set; } = 0;

    public StringOfServiceObservableCollection(IServiceStringService stringOfServiceService)
    {
        this.stringService = stringOfServiceService;
        Init();
    }

    public void Add(StringOfServiceModel stringOfService)
    {
        stringService.Create(stringOfService);
        Init();
    }

    public void Update(StringOfServiceModel stringOfService)
    {
        stringService.Update(stringOfService);
        Init();
    }

    public StringOfServiceModel GetStringOfService(int id)
    {
        return stringService.GetById(id);
    }

    public void Delete(int id)
    {
        stringService.Delete(id); 
        Init();
    }

    public void Refresh()
    {
        Init();
    }

    public void Filter(string searchString)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        var all = stringService.GetAll();

        if (string.IsNullOrWhiteSpace(searchString))
        {
            strings = new ObservableCollection<StringOfServiceModel>(all);
        }
        else
        {
            var filtered = all
                .Where(x => x.Id.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                x.DateString.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                x.Distance.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            strings = new ObservableCollection<StringOfServiceModel>(filtered);
        }
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { strings });
        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void Init()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        this.strings = new ObservableCollection<StringOfServiceModel>(stringService.GetAll());
        OnPropertyChanged(NotifyCollectionChangedAction.Add, new[] { strings });

        stopwatch.Stop();
        ExecutionTimeLastCommand = stopwatch.ElapsedMilliseconds;
        ExecutionTimeUpdated?.Invoke(ExecutionTimeLastCommand);
    }

    private void OnPropertyChanged(NotifyCollectionChangedAction action, IList changedItems)
    {
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action, changedItems));
    }
}
