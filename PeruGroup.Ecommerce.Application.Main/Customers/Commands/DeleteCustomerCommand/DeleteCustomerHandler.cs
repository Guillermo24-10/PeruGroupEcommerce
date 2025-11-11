using AutoMapper;
using MediatR;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var result = await _unitOfWork.CustomersRepository.DeleteAsync(request.CustomerId!);
            if (!result)
            {
                response.IsSuccess = false;
                response.Message = "No se pudo eliminar el customer. Verifique si el ID es correcto.";
                return response;
            }
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "Se eliminó customer correctamente.";

            return response;
        }
    }
}
