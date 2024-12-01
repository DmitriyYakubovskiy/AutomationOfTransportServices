using AutomationOfTransportServices.Collections;
using AutomationOfTransportServices.Commands;
using AutomationOfTransportServices.Models;
using AutomationOfTransportServices.Services;
using AutomationOfTransportServices.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class TypesOfServicesViewModel : INotifyPropertyChanged
{
    private TypeOfServiceObservableCollection types;
    private Command addTypeCommand;
    private Command editTypeCommand;
    private Command deleteTypeCommand;
    private Command searchTypeCommand;
    private Command refreshTypesCommand;
    private Window thisWindow;
    private IServiceTypeService typeService;
    private string searchText;
    private string executionTime;

    public ICommand AddTypeCommand => addTypeCommand;
    public ICommand EditTypeCommand => editTypeCommand;
    public ICommand DeleteTypeCommand => deleteTypeCommand;
    public ICommand SearchTypeCommand => searchTypeCommand;
    public ICommand RefreshTypesCommand => refreshTypesCommand;

    public ObservableCollection<TypeOfServiceModel> Types => types.Types;

    public string ExecutionTime
    {
        get => executionTime + " ms";
        set
        {
            if (executionTime != value)
            {
                executionTime = value;
                OnPropertyChanged(nameof(ExecutionTime));
            }
        }
    }

    public string SearchText
    {
        get => searchText;
        set
        {
            searchText = value;
            OnPropertyChanged(nameof(SearchText));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public TypesOfServicesViewModel(Window window, IServiceTypeService typeService)
    {
        this.typeService = typeService;
        types = new TypeOfServiceObservableCollection(typeService);
        thisWindow = window;

        types.CollectionChanged += (_, e) =>
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnPropertyChanged(nameof(Types));
            }
        };
        types.ExecutionTimeUpdated += (newExecutionTime) =>
        {
            ExecutionTime = newExecutionTime.ToString();
        };
        ExecutionTime = types.ExecutionTimeLastCommand.ToString();

        addTypeCommand = new DelegateCommand(_ => AddType());
        editTypeCommand = new GenericCommand<TypeOfServiceModel>(x => EditType(x), x => x != null);
        deleteTypeCommand = new GenericCommand<TypeOfServiceModel>(x => DeleteType(x), x => x != null);
        searchTypeCommand = new DelegateCommand(_ => SearchTypes());
        refreshTypesCommand = new DelegateCommand(_ => Refresh());
    }

    private void SearchTypes()
    {
        types.Filter(searchText);
    }

    private void Refresh()
    {        
        SearchText = "";
        types.Refresh();
    }

    private void AddType()
    {
        var window = new AddEditTypeOfServiceView(thisWindow);
        var typeDetailsViewModel = new AddEditTypeOfServiceViewModel(window, typeService);
        window.DataContext = typeDetailsViewModel;

        if (window.ShowDialog() == true)
        {
            var newType = typeDetailsViewModel.Type;
            if (newType != null) types.Add(newType);
        }
    }

    private void EditType(TypeOfServiceModel model)
    {
        if (model != null)
        {
            var window = new AddEditTypeOfServiceView(thisWindow);
            var typeDetailsViewModel = new AddEditTypeOfServiceViewModel(window, typeService, model.Clone() as TypeOfServiceModel);
            window.DataContext = typeDetailsViewModel;

            if (window.ShowDialog() == true)
            {
                var newType = typeDetailsViewModel.Type;
                if (newType != null) types.Update(newType);
            }
        }
    }

    private void DeleteType(TypeOfServiceModel model)
    {
        if (model == null) return;
        if (MessageBox.Show("Вы уверены, что хотите удалить этот тип?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) types.Delete(model.Id);
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
