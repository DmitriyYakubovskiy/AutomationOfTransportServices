using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface IClientService
{
    ClientModel[] GetAll();
    ClientModel GetById(int id);
    void Create(ClientModel model);
    void Update(ClientModel model);
    void Delete(int id);
}