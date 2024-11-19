using AutoMapper;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.DataAccess.Repositories;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository clientRepository;
    private readonly IMapper mapper;

    public ClientService(IClientRepository clientRepository, IMapper mapper)
    {
        this.clientRepository = clientRepository;
        this.mapper = mapper;
    }

    public void Create(ClientModel model)
    {
        clientRepository.Create(mapper.Map<ClientEntity>(model));
    }

    public void Delete(int id)
    {
        clientRepository.Delete(id);
    }

    public ClientModel[] GetAll()
    {
        return mapper.Map<ClientModel[]>(clientRepository.GetAll());
    }

    public ClientModel GetById(int id)
    {
        var entity = clientRepository.GetById(id);
        return mapper.Map<ClientModel>(entity);
    }

    public void Update(ClientModel model)
    {
        var oldEntity = clientRepository.GetById(model.Id);
        if (oldEntity == null) return;
        oldEntity.Id = model.Id;
        oldEntity.Name = model.Name;
        oldEntity.NumberOfTelephone = model.NumberOfTelephone;

        clientRepository.Update(oldEntity);
    }
}