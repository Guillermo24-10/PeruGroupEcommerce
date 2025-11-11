using AutoMapper;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Domain.Events;
using Disco = PeruGroup.Ecommerce.Domain.Entities.Discount;

namespace PeruGroup.Ecommerce.Application.UseCases.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Disco, DiscountDto>().ReverseMap();
            CreateMap<Disco, DiscountCreatedEvent>().ReverseMap();


            CreateMap<Customer, CreateCustomerCommand>().ReverseMap();
            CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();

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
