using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для ClientDetailsView.xaml
/// </summary>
public partial class AddEditClientView : Window
{
    public AddEditClientView(Window owner)
    {
        InitializeComponent();
        Owner=owner;
    }
}
