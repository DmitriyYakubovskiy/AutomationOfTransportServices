using AutomationOfTransportServices.DataAccess.Contexts;
using AutomationOfTransportServices.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutomationOfTransportServices.DataAccess.Repositories;

public class TypeOfServiceRepository : ITypeOfServiceRepository
{
    private readonly TransportServicesDbContext dbContext;

    public TypeOfServiceRepository(TransportServicesDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(TypeOfServiceEntity entity)
    {
        dbContext.TypesOfServices.Add(entity);
        dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity == null) return;
        var strings = dbContext.Strings.Where(x => x.TypeOfServiceId == id).ToList();
        foreach (var @string in strings)
        {
            @string.TypeOfServiceId = null!;
        }
        dbContext.TypesOfServices.Remove(entity);
        dbContext.SaveChanges();
    }

    public TypeOfServiceEntity[] GetAll(string searchString = null!)
    {
        return dbContext.TypesOfServices.ToArray();
    }

    public TypeOfServiceEntity GetById(int id)
    {
        return dbContext.TypesOfServices
            .Include(x => x.Strings).ThenInclude(d => d.Driver)
            .Include(x => x.Strings).ThenInclude(v => v.Vehicle)
            .Include(x => x.Strings).ThenInclude(t => t.Client)
            .FirstOrDefault(x => x.Id == id)!;
    }

    public void Update(TypeOfServiceEntity entity)
    {
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
