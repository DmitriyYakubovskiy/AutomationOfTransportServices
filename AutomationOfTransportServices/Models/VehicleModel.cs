using System.ComponentModel;

namespace AutomationOfTransportServices.Models;

public class VehicleModel : INotifyPropertyChanged, ICloneable
{
    private int id;
    private string name = null!;
    private int priceOfKm;
    public List<StringOfServiceModel> Strings = new List<StringOfServiceModel>();

    public int Id
    {
        get => id;
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }

    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public int PriceOfKm
    {
        get => priceOfKm;
        set
        {
            if (priceOfKm != value)
            {
                priceOfKm = value;
                OnPropertyChanged(nameof(PriceOfKm));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public object Clone()
    {
        return new VehicleModel
        {
            Id = this.Id,
            Name = this.Name
        };
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
