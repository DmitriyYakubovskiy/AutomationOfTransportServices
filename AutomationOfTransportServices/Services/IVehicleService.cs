using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface IVehicleService
{
    VehicleModel[] GetAll();
    VehicleModel GetById(int id);
    void Create(VehicleModel model);
    void Update(VehicleModel model);
    void Delete(int id);
}