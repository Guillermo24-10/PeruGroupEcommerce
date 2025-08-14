using AutoMapper;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Domain.Entity;

namespace PeruGroup.Ecommerce.Transversal.Mapper
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customers, CutomersDto>().ReverseMap();
            CreateMap<Users,UsersDTO>().ReverseMap();
            CreateMap<Categories, CategoriesDto>().ReverseMap();
            //CreateMap<Customers, CutomersDto>().ReverseMap()
            //    .ForMember(destino => destino.CustomerId, source => source.MapFrom(origen => origen.CustomerId))
            //    .ForMember(destino => destino.CompanyName, source => source.MapFrom(origen => origen.CompanyName))
            //    .ForMember(destino => destino.ContactName, source => source.MapFrom(origen => origen.ContactName))
            //    .ForMember(destino => destino.ContactTitle, source => source.MapFrom(origen => origen.ContactTitle))
            //    .ForMember(destino => destino.Address, source => source.MapFrom(origen => origen.Address))
            //    .ForMember(destino => destino.City, source => source.MapFrom(origen => origen.City))
            //    .ForMember(destino => destino.Region, source => source.MapFrom(origen => origen.Region))
            //    .ForMember(destino => destino.PostalCode, source => source.MapFrom(origen => origen.PostalCode))
            //    .ForMember(destino => destino.Country, source => source.MapFrom(origen => origen.Country))
            //    .ForMember(destino => destino.Phone, source => source.MapFrom(origen => origen.Phone))
            //    .ForMember(destino => destino.Fax, source => source.MapFrom(origen => origen.Fax));
        }
    }
}
