using AutomationOfTransportServices.Models;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using AutomationOfTransportServices.DataAccess.Helpers;
using AutomationOfTransportServices.Services;

namespace AutomationOfTransportServices.ViewModels;

public class AddEditTypeOfServiceViewModel : INotifyPropertyChanged
{
    private TypeOfServiceModel type;
    private Window thisWindow;
    private IServiceTypeService typeService;

    public TypeOfServiceModel Type
    {
        get => type;
        set
        {
            type = value;
            OnPropertyChanged(nameof(Type));
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public AddEditTypeOfServiceViewModel(Window window, IServiceTypeService typeService, TypeOfServiceModel existingTypeOfService = null!)
    {
        this.typeService = typeService;
        thisWindow = window;
        Type = existingTypeOfService ?? new TypeOfServiceModel();

        SaveCommand = new DelegateCommand(_ => Save());
        CancelCommand = new DelegateCommand(_ => Cancel());
    }

    private void Save()
    {
        if (String.IsNullOrEmpty(Type.Name?.Trim()) || Type?.Name.Length > EntityRestrictions.typeOfServiceNameLength) MessageBox.Show($"Неверная длина названия!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (typeService.GetAll().Where(x => x.Name == Type.Name).FirstOrDefault() != null) MessageBox.Show("Такой тип уже есть!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else thisWindow.DialogResult = true;
    }

    private void Cancel()
    {
        thisWindow.DialogResult = false;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
