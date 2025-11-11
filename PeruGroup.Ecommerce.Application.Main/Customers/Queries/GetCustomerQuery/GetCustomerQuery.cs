using MediatR;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery
{
    public sealed record GetCustomerQuery : IRequest<Response<CustomerDto>>
    {
        public string? CustomerId { get; set; }
    }
}
