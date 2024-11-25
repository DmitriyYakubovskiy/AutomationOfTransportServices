using AutoMapper;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public class ServiceStringService : IServiceStringService
{
    private readonly IStringOfServiceRepository stringOfServiceRepository;
    private readonly IMapper mapper;

    public ServiceStringService(IStringOfServiceRepository stringOfServiceRepository, IMapper mapper)
    {
        this.stringOfServiceRepository = stringOfServiceRepository;
        this.mapper = mapper;
    }

    public void Create(StringOfServiceModel model)
    {
        stringOfServiceRepository.Create(mapper.Map<StringOfServiceEntity>(model));
    }

    public void Delete(int id)
    {
        stringOfServiceRepository.Delete(id);
    }

    public StringOfServiceModel[] GetAll()
    {
        return mapper.Map<StringOfServiceModel[]>(stringOfServiceRepository.GetAll());
    }

    public StringOfServiceModel GetById(int id)
    {
        var entity = stringOfServiceRepository.GetById(id);
        return mapper.Map<StringOfServiceModel>(entity);
    }

    public void Update(StringOfServiceModel model)
    {
        var oldEntity = stringOfServiceRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        //oldEntity.Name = model.Name;
        //oldEntity.NumberOfTelephone = model.NumberOfTelephone;

        stringOfServiceRepository.Update(oldEntity);
    }
}