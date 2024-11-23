using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для ClientsView.xaml
/// </summary>
public partial class ClientsView : Window
{
    public ClientsView(Window owner)
    {
        InitializeComponent();
        Owner = owner;
    }
}
