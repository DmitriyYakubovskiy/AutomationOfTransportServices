using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для DriverStatisticsView.xaml
/// </summary>
public partial class DriverStatisticsView : Window
{
    public DriverStatisticsView(Window owner)
    {
        InitializeComponent();
        Owner = owner;
    }
}
