using AutoMapper;
using MediatR;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(request);
            var result = await _unitOfWork.CustomersRepository.InsertAsync(customer);
            if (!result)
            {
                response.IsSuccess = false;
                response.Message = "No se pudo insertar el customer. Verifique los datos.";
                return response;
            }
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "Se insertó customer correctamente.";

            return response;
        }
    }
}
