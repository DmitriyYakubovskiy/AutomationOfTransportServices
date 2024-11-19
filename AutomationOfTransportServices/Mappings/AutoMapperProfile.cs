using AutoMapper;
using System.Windows;
using AutomationOfTransportServices.DataAccess.Entities;
using AutomationOfTransportServices.Models;

namespace AutomationOfTransportServices.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StringOfServiceModel, StringOfServiceEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.Client, opt => opt.Ignore()) 
            .ForMember(dest => dest.TypeOfService, opt => opt.Ignore())
            .ForMember(dest => dest.Vehicle, opt => opt.Ignore())
            .ForMember(dest => dest.Driver, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Distance, opt => opt.MapFrom(src => src.Distance))
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.TypeOfService, opt => opt.MapFrom(src => src.TypeOfService))
            .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
            .ForMember(dest => dest.Driver, opt => opt.MapFrom(src => src.Driver));

        CreateMap<ClientModel, ClientEntity>().ReverseMap();
        CreateMap<TypeOfServiceModel, TypeOfServiceEntity>().ReverseMap();
        CreateMap<VehicleModel, VehicleEntity>().ReverseMap();  
        CreateMap<DriverModel, DriverEntity>().ReverseMap();
    }
}
