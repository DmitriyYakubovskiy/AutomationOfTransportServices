namespace AutomationOfTransportServices.Models;

public class ClientModel:ICloneable
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NumberOfTelephone { get; set; } = null!;
    public List<StringOfServiceModel> Strings { get; set; } = new List<StringOfServiceModel>();

    public object Clone()
    {
        return new ClientModel
        {
            Id = this.Id,
            Name = this.Name,
            NumberOfTelephone = this.NumberOfTelephone,
        };
    }
}