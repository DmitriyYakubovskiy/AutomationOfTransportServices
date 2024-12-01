using AutomationOfTransportServices.DataAccess.Helpers;
using AutomationOfTransportServices.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace AutomationOfTransportServices.ViewModels;

public class AddEditClientViewModel : INotifyPropertyChanged
{
    private ClientModel client;
    private Window thisWindow;

    public ClientModel Client
    {
        get => client;
        set
        {
            client = value;
            OnPropertyChanged(nameof(Client));
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public AddEditClientViewModel(Window window, ClientModel existingClient = null!)
    {
        thisWindow= window;
        Client = existingClient ?? new ClientModel();

        SaveCommand = new DelegateCommand(_ => Save());
        CancelCommand = new DelegateCommand(_ => Cancel());
    }

    private void Save()
    {
        if (String.IsNullOrEmpty(Client.Name?.Trim()) || Client?.Name.Length > EntityRestrictions.clientNameLength) MessageBox.Show($"Неверная длина имени!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        else if (String.IsNullOrEmpty(Client.NumberOfTelephone?.Trim()) || Client?.NumberOfTelephone?.Trim().Length != EntityRestrictions.clientTelephoneLength) MessageBox.Show($"Неверная длина телефона! Длина должна быть {EntityRestrictions.clientTelephoneLength} символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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