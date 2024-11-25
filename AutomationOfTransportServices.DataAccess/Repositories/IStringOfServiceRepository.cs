using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface IStringOfServiceRepository
{
    StringOfServiceEntity[] GetAll(string searchString = null!);
    StringOfServiceEntity GetById(int id);
    void Create(StringOfServiceEntity entity);
    void Update(StringOfServiceEntity entity);
    void Delete(int id);
}
