using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface IVehicleRepository
{
    VehicleEntity[] GetAll();
    VehicleEntity GetById(int id);
    void Create(VehicleEntity entity);
    void Update(VehicleEntity entity);
    void Delete(int id);
}
