using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface IClientService
{
    ClientModel[] GetAll(string searchString = null!);
    ClientModel GetById(int id);
    void Create(ClientModel model);
    void Update(ClientModel model);
    void Delete(int id);
}