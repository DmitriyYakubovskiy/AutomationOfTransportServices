namespace AutomationOfTransportServices.Models;

public class TypeOfServiceModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<StringOfServiceModel> Strings { get; set; } = new List<StringOfServiceModel>();
}
