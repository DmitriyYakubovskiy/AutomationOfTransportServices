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
        thisWindow.DialogResult = true;
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