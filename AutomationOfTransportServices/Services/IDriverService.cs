using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public interface IDriverService
{
    DriverModel[] GetAll();
    DriverModel GetById(int id);
    void Create(DriverModel model);
    void Update(DriverModel model);
    void Delete(int id);
}