using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для ClientDetailsView.xaml
/// </summary>
public partial class ClientDetailsView : Window
{
    public ClientDetailsView(Window owner)
    {
        InitializeComponent();
        Owner=owner;
    }
}
