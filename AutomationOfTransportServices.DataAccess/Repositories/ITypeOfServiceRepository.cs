using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface ITypeOfServiceRepository
{
    TypeOfServiceEntity[] GetAll();
    TypeOfServiceEntity GetById(int id);
    void Create(TypeOfServiceEntity entity);
    void Update(TypeOfServiceEntity entity);
    void Delete(int id);
}
