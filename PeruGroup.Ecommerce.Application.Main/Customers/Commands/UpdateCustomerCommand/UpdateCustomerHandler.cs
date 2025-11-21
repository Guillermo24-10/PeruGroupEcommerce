using AutoMapper;
using MediatR;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Domain.Specifications;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(request);
            //uso del patron specification para validaciones de negocio
            var countryInBlackListSpec = new CountryInBlackListSpecification();
            if (!countryInBlackListSpec.IsSatisfiedBy(customer))
            {
                response.IsSuccess = false;
                response.Message = $"Los clientes del pais {customer.Country} no se pueden actualizar porque se encuentra en lista negra.";
                return response;
            }

            var result = await _unitOfWork.CustomersRepository.UpdateAsync(customer);
            if (!result)
            {
                response.IsSuccess = false;
                response.Message = "No se pudo actualizar el customer. Verifique los datos.";
                return response;
            }
            response.Data = result;
            response.IsSuccess = true;
            response.Message = "Se actualizó customer correctamente.";

            return response;
        }
    }
}
