using AutoMapper;
using MediatR;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Response<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<CustomerDto>();

            var customer = await _unitOfWork.CustomersRepository.GetByIdAsync(request.CustomerId!);
            if (customer == null)
            {
                response.IsSuccess = false;
                response.Message = "Customer no encontrado.";
                return response;
            }
            response.Data = _mapper.Map<CustomerDto>(customer);
            response.IsSuccess = true;
            response.Message = "Se obtuvo customer correctamente.";

            return response;
        }
    }
}
