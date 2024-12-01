using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для ServiceTypeStatisticsView.xaml
/// </summary>
public partial class ServiceTypeStatisticsView : Window
{
    public ServiceTypeStatisticsView(Window window)
    {
        InitializeComponent();
        Owner = window;
    }
}
