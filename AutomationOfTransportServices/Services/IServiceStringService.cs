using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface IServiceStringService
{
    StringOfServiceModel[] GetAll();
    StringOfServiceModel GetById(int id);
    void Create(StringOfServiceModel model);
    void Update(StringOfServiceModel model);
    void Delete(int id);
}