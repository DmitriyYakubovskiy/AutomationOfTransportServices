using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface ITypeOfServiceRepository
{
    TypeOfServiceEntity[] GetAll(string searchString = null!);
    TypeOfServiceEntity GetById(int id);
    void Create(TypeOfServiceEntity entity);
    void Update(TypeOfServiceEntity entity);
    void Delete(int id);
}
