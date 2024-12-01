using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для AddEditDriverView.xaml
/// </summary>
public partial class AddEditDriverView : Window
{
    public AddEditDriverView(Window window)
    {
        InitializeComponent();
        Owner = window;
    }
}
