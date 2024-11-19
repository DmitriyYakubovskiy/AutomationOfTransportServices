using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly TransportServicesDbContext dbContext;

    public ClientRepository(TransportServicesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(ClientEntity entity)
    {
        dbContext.Clients.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Clients.Remove(entity);
        dbContext.SaveChanges();
    }

    public ClientEntity[] GetAll()
    {
        return dbContext.Clients.ToArray();
    }

    public ClientEntity GetById(int id)
    {
        return dbContext.Clients.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(ClientEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}