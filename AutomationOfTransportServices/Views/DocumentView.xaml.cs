using System.Windows;

namespace AutomationOfTransportServices.Views;

/// <summary>
/// Логика взаимодействия для DocumentView.xaml
/// </summary>
public partial class DocumentView : Window
{
    public DocumentView(Window owner)
    {
        InitializeComponent();
        Owner = owner;  
    }
}
