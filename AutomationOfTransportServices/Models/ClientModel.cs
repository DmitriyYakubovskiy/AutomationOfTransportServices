namespace AutomationOfTransportServices.Models;

public class ClientModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NumberOfTelephone { get; set; } = null!;
    public List<StringOfServiceModel> Strings { get; set; } = new List<StringOfServiceModel>();
}
