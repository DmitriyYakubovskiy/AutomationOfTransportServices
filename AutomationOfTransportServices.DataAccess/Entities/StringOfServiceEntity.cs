namespace AutomationOfTransportServices.DataAccess.Entities;

public class StringOfServiceEntity
{
    public int Id { get; set; }
    public int Number { get; set; }
    public DateOnly Date { get; set; }
    public int Distance { get; set; }
    public int? ClientId { get; set; }
    public int? TypeOfServiceId { get; set; }
    public int? VehicleId { get; set; }
    public int? DriverId { get; set; }
    public virtual ClientEntity Client { get; set; } = null!;
    public virtual TypeOfServiceEntity TypeOfService { get; set; } = null!;
    public virtual VehicleEntity Vehicle { get; set; } = null!;
    public virtual DriverEntity Driver { get; set; } = null!;
}
