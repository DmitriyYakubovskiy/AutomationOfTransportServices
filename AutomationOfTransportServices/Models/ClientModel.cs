using System.ComponentModel;

namespace AutomationOfTransportServices.Models;

public class ClientModel : INotifyPropertyChanged, ICloneable
{
    private int id;
    private string name;
    private string numberOfTelephone;

    public int Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string NumberOfTelephone
    {
        get => numberOfTelephone;
        set
        {
            numberOfTelephone = value;
            OnPropertyChanged(nameof(NumberOfTelephone));
        }
    }

    public List<StringOfServiceModel> Strings { get; set; } = new List<StringOfServiceModel>();

    public event PropertyChangedEventHandler PropertyChanged;

    public object Clone()
    {
        return new ClientModel
        {
            Id = this.Id,
            Name = this.Name,
            NumberOfTelephone = this.NumberOfTelephone,
        };
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}