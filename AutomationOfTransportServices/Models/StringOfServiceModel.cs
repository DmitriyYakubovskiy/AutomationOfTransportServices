namespace AutomationOfTransportServices.Models;

public class StringOfServiceModel
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateOnly Date { get; set; }
    public int Distance { get; set; }
    public int ClientId { get; set; }
    public int TypeOfServiceId { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public ClientModel Client { get; set; } = null!;
    public TypeOfServiceModel TypeOfService { get; set; } = null!;
    public VehicleModel Vehicle { get; set; } = null!;
    public DriverModel Driver { get; set; } = null!;
}
