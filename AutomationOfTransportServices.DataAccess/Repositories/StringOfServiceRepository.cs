using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;

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

    public StringOfServiceEntity[] GetAll()
    {
        return dbContext.Strings.ToArray();
    }

    public StringOfServiceEntity GetById(int id)
    {
        return dbContext.Strings.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(StringOfServiceEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
