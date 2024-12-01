using System.ComponentModel;

namespace AutomationOfTransportServices.Models;

public class StringOfServiceModel : INotifyPropertyChanged, ICloneable
{
    private int id;
    private int number;
    private DateOnly date;
    private int distance;
    private int? clientId;
    private int? typeOfServiceId;
    private int? vehicleId;
    private int? driverId;
    private ClientModel client;
    private TypeOfServiceModel typeOfService;
    private VehicleModel vehicle;
    private DriverModel driver;

    public int Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public int Number
    {
        get => number;
        set
        {
            number = value;
            OnPropertyChanged(nameof(Number));
        }
    }

    public string DateString
    {
        get => date.ToString("d.M.yyyy");
        set { }
    }

    public DateOnly Date
    {
        get => date;
        set
        {
            date = value;
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateTime));
        }
    }

    public DateTime DateTime
    {
        get => date.ToDateTime(TimeOnly.MinValue);
        set
        {
            date = DateOnly.FromDateTime(value);
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateTime));
        }
    }

    public int Distance
    {
        get => distance;
        set
        {
            distance = value;
            OnPropertyChanged(nameof(Distance));
            OnPropertyChanged(nameof(Cost));
        }
    }

    public int Cost
    {
        get
        {
            if(vehicle == null) return 0;
            else return Distance* Vehicle.PriceOfKm;
        }
        set { }
            
    }
    public int? ClientId
    {
        get => clientId;
        set
        {
            clientId = value;
            OnPropertyChanged(nameof(ClientId));
        }
    }

    public int? TypeOfServiceId
    {
        get => typeOfServiceId;
        set
        {
            typeOfServiceId = value;
            OnPropertyChanged(nameof(TypeOfServiceId));
        }
    }

    public int? VehicleId
    {
        get => vehicleId;
        set
        {
            vehicleId = value;
            OnPropertyChanged(nameof(VehicleId));
            OnPropertyChanged(nameof(Cost));
        }
    }

    public int? DriverId
    {
        get => driverId;
        set
        {
            driverId = value;
            OnPropertyChanged(nameof(DriverId));
        }
    }

    public ClientModel Client
    {
        get => client;
        set
        {
            client = value;
            if(value==null) ClientId = null;
            else ClientId = client.Id;
            OnPropertyChanged(nameof(Client));
        }
    }

    public TypeOfServiceModel TypeOfService
    {
        get => typeOfService;
        set
        {
            typeOfService = value; 
            if (value == null) TypeOfServiceId = null;
            else TypeOfServiceId = value.Id;
            OnPropertyChanged(nameof(TypeOfService));
        }
    }

    public VehicleModel Vehicle
    {
        get => vehicle;
        set
        {
            vehicle = value; 
            if (value == null) VehicleId = null;
            else VehicleId = value.Id;
            OnPropertyChanged(nameof(Vehicle));
        }
    }

    public DriverModel Driver
    {
        get => driver;
        set
        {
            driver = value; 
            if (value == null) DriverId = null;
            else DriverId = value.Id;
            OnPropertyChanged(nameof(Driver));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public object Clone()
    {
        return new StringOfServiceModel
        {
            Id = this.Id,
            Number = this.Number,
            Date = this.Date,
            Distance = this.Distance,
            Driver = this.Driver,
            Client = this.Client,
            Vehicle= this.Vehicle,
            TypeOfService = this.TypeOfService,
        };
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}