using AutoMapper;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public class TypeOfServiceService : ITypeOfServiceService
{
    private readonly ITypeOfServiceRepository typeOfServiceRepository;
    private readonly IMapper mapper;

    public TypeOfServiceService(ITypeOfServiceRepository typeOfServiceRepository, IMapper mapper)
    {
        this.typeOfServiceRepository = typeOfServiceRepository;
        this.mapper = mapper;
    }

    public void Create(TypeOfServiceModel model)
    {
        typeOfServiceRepository.Create(mapper.Map<TypeOfServiceEntity>(model));
    }

    public void Delete(int id)
    {
        typeOfServiceRepository.Delete(id);
    }

    public TypeOfServiceModel[] GetAll()
    {
        return mapper.Map<TypeOfServiceModel[]>(typeOfServiceRepository.GetAll());
    }

    public TypeOfServiceModel GetById(int id)
    {
        var entity = typeOfServiceRepository.GetById(id);
        return mapper.Map<TypeOfServiceModel>(entity);
    }

    public void Update(TypeOfServiceModel model)
    {
        var oldEntity = typeOfServiceRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Name = model.Name;

        typeOfServiceRepository.Update(oldEntity);
    }
}