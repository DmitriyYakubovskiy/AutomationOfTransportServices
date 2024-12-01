using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для VehicleStatisticsView.xaml
/// </summary>
public partial class VehicleStatisticsView : Window
{
    public VehicleStatisticsView(Window owner)
    {
        InitializeComponent();
        Owner = owner;
    }
}
