using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface IDriverRepository
{
    DriverEntity[] GetAll(string searchString = null!);
    DriverEntity GetById(int id);
    void Create(DriverEntity entity);
    void Update(DriverEntity entity);
    void Delete(int id);
}
