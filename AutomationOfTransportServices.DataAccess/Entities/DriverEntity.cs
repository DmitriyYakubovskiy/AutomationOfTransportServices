namespace AutomationOfTransportServices.DataAccess.Entities;

public class DriverEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<StringOfServiceEntity> Strings { get; set; } = new List<StringOfServiceEntity>();
}
