using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly TransportServicesDbContext dbContext;

    public VehicleRepository(TransportServicesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(VehicleEntity entity)
    {
        dbContext.Vehicles.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        dbContext.Vehicles.Remove(entity);
        dbContext.SaveChanges();
    }

    public VehicleEntity[] GetAll(string searchString = null!)
    {
        return dbContext.Vehicles.ToArray();
    }

    public VehicleEntity GetById(int id)
    {
        return dbContext.Vehicles.Include(x => x.Strings).ThenInclude(d => d.Driver)
            .Include(x => x.Strings).ThenInclude(c => c.Client)
            .Include(x => x.Strings).ThenInclude(t => t.TypeOfService).FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(VehicleEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
