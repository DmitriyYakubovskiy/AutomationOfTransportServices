namespace AutomationOfTransportServices.Models;

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PriceOfKm { get; set; }
    public List<StringOfServiceModel> Strings { get; set; } = new List<StringOfServiceModel>();
}
