using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public interface IClientRepository
{
    ClientEntity[] GetAll();
    ClientEntity GetById(int id);
    void Create(ClientEntity entity);
    void Update(ClientEntity entity);
    void Delete(int id);
}
