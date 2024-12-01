﻿namespace AutomationOfTransportServices.Models;

public class VehicleStatisticModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int NumberOfServices { get; set; }
    public decimal TotalCost { get; set; }
}