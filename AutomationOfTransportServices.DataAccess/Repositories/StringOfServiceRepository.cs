using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public class StringOfServiceRepository : IStringOfServiceRepository
{
    private readonly TransportServicesDbContext dbContext;

    public StringOfServiceRepository(TransportServicesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(StringOfServiceEntity entity)
    {
        int N = GetAll().Where(x => x.ClientId == entity.ClientId).Select(x => x.Number).DefaultIfEmpty(0).Max() + 1;
        entity.Number = N;
        dbContext.Strings.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Strings.Remove(entity);
        dbContext.SaveChanges();
    }

    public StringOfServiceEntity[] GetAll(string searchString = null!)
    {
        return dbContext.Strings.Include(x => x.Client).Include(x => x.Driver).Include(x => x.TypeOfService).Include(x => x.Vehicle).ToArray();
    }

    public StringOfServiceEntity GetById(int id)
    {
        return dbContext.Strings.Include(x=>x.Client).Include(x => x.Driver).Include(x => x.TypeOfService).Include(x => x.Vehicle).FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(StringOfServiceEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
