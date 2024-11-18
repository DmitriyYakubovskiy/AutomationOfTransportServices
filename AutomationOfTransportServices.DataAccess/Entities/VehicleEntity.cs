namespace AutomationOfTransportServices.DataAccess.Entities;

public class VehicleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PriceOfKm { get; set; }
    public virtual ICollection<StringOfServiceEntity> Strings { get; set; } = new List<StringOfServiceEntity>();
}
