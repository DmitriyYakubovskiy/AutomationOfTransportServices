using AutomationOfTransportServices.DataAccess.Helpers;
using AutomationOfTransportServices.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

class AddEditDriverViewModel : INotifyPropertyChanged
{
    private DriverModel driver;
    private Window thisWindow;

    public DriverModel Driver
    {
        get => driver;
        set
        {
            driver = value;
            OnPropertyChanged(nameof(Driver));
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public AddEditDriverViewModel(Window window, DriverModel existingDriver = null!)
    {
        thisWindow = window;
        Driver = existingDriver ?? new DriverModel();

        SaveCommand = new DelegateCommand(_ => Save());
        CancelCommand = new DelegateCommand(_ => Cancel());
    }

    private void Save()
    {
        if (String.IsNullOrEmpty(Driver.Name?.Trim()) || Driver?.Name.Length > EntityRestrictions.clientNameLength) MessageBox.Show($"Неверная длина имени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
