namespace AutomationOfTransportServices.DataAccess.Entities;

public class StringOfServiceEntity
{
    public string Id { get; set; }
    public int Number { get; set; }
    public string Date { get; set; }
    public int Distance { get; set; }
    public int ClientId { get; set; }
    public int TypeOfServiceId { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; }
    public virtual ClientEntity Clinet { get; set; } = null!;
    public virtual TypeOfServiceEntity TypeOfService { get; set; } = null!;
    public virtual VehicleEntity Vehicle { get; set; } = null!;
    public virtual DriverEntity Driver { get; set; } = null!;
}
