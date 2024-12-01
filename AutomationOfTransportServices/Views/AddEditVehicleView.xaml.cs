using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для AddEditVehicleView.xaml
/// </summary>
public partial class AddEditVehicleView : Window
{
    public AddEditVehicleView(Window owner)
    {
        InitializeComponent();
        Owner= owner;
    }
}
