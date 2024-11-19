using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly TransportServicesDbContext dbContext;

    public DriverRepository(TransportServicesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(DriverEntity entity)
    {
        dbContext.Drivers.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Drivers.Remove(entity);
        dbContext.SaveChanges();
    }

    public DriverEntity[] GetAll()
    {
        return dbContext.Drivers.ToArray();
    }

    public DriverEntity GetById(int id)
    {
        return dbContext.Drivers.FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(DriverEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}