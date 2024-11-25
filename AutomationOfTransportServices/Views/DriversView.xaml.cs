using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для DriversView.xaml
/// </summary>
public partial class DriversView : Window
{
    public DriversView(Window owner)
    {
        InitializeComponent();
        Owner= owner;   
    }
}
