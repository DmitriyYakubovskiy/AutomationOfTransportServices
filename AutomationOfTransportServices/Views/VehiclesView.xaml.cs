using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для VehiclesView.xaml
/// </summary>
public partial class VehiclesView : Window
{
    public VehiclesView(Window owner)
    {
        InitializeComponent();
        Owner = owner;
    }
}
