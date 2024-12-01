using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для ClientStatisticsView.xaml
/// </summary>
public partial class ClientStatisticsView : Window
{
    public ClientStatisticsView(Window owner)
    {
        InitializeComponent();
        Owner = owner;
    }
}
