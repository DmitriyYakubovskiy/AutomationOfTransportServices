using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

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

    public ClientEntity[] GetAll(string searchString = null!)
    {
        if(searchString== null) return dbContext.Clients.ToArray();
        return dbContext.Clients.Where(client => client.Name.Contains(searchString) || client.NumberOfTelephone.Contains(searchString)).ToArray();
    }

    public ClientEntity GetById(int id)
    {
        return dbContext.Clients
            .Include(x=>x.Strings).ThenInclude(d=>d.Driver)
            .Include(x=>x.Strings).ThenInclude(v=>v.Vehicle)
            .Include(x=>x.Strings).ThenInclude(t=>t.TypeOfService)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(ClientEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}