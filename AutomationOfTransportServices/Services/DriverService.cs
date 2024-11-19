using AutoMapper;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository driverRepository;
    private readonly IMapper mapper;

    public DriverService(IDriverRepository driverRepository, IMapper mapper)
    {
        this.driverRepository = driverRepository;
        this.mapper = mapper;
    }

    public void Create(DriverModel model)
    {
        driverRepository.Create(mapper.Map<DriverEntity>(model));
    }

    public void Delete(int id)
    {
        driverRepository.Delete(id);
    }

    public DriverModel[] GetAll()
    {
        return mapper.Map<DriverModel[]>(driverRepository.GetAll());
    }

    public DriverModel GetById(int id)
    {
        var entity = driverRepository.GetById(id);
        return mapper.Map<DriverModel>(entity);
    }

    public void Update(DriverModel model)
    {
        var oldEntity = driverRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Name = model.Name;

        driverRepository.Update(oldEntity);
    }
}