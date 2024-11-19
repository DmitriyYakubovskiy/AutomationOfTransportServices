using AutoMapper;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository vehicleRepository;
    private readonly IMapper mapper;

    public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper)
    {
        this.vehicleRepository = vehicleRepository;
        this.mapper = mapper;
    }

    public void Create(VehicleModel model)
    {
        vehicleRepository.Create(mapper.Map<VehicleEntity>(model));
    }

    public void Delete(int id)
    {
        vehicleRepository.Delete(id);
    }

    public VehicleModel[] GetAll()
    {
        return mapper.Map<VehicleModel[]>(vehicleRepository.GetAll());
    }

    public VehicleModel GetById(int id)
    {
        var entity = vehicleRepository.GetById(id);
        return mapper.Map<VehicleModel>(entity);
    }

    public void Update(VehicleModel model)
    {
        var oldEntity = vehicleRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Name = model.Name;
        oldEntity.PriceOfKm = model.PriceOfKm;

        vehicleRepository.Update(oldEntity);
    }
}