namespace AutomationOfTransportServices.Models;

public class ClientStatisticModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int NumberOfServices { get; set; }
    public int TotalCost { get; set; }
}