using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface ITypeOfServiceService
{
    TypeOfServiceModel[] GetAll();
    TypeOfServiceModel GetById(int id);
    void Create(TypeOfServiceModel model);
    void Update(TypeOfServiceModel model);
    void Delete(int id);
}