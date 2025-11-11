using AutoMapper;
using Microsoft.Extensions.Logging;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Interface.UseCases;
using PeruGroup.Ecommerce.Domain.Entities;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Customers
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersApplication> _logger;

        public CustomersApplication(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CustomersApplication> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> DeleteAsync(string id)
        {
            var response = new Response<bool>();
            var result = await _unitOfWork.CustomersRepository.DeleteAsync(id);
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

        public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDto>>();
            var customers = await _unitOfWork.CustomersRepository.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            response.IsSuccess = true;
            response.Message = "Se obtuvieron todos los customers correctamente.";
            _logger.LogInformation("Se obtuvieron todos los customers correctamente.");

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CustomerDto>>();

            var count = await _unitOfWork.CustomersRepository.CountAsync();
            var customers = await _unitOfWork.CustomersRepository.GetAllWithPaginationAsync(pageNumber, pageSize);
            response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            if (response.Data != null)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.Message = "Se obtuvieron los customers con paginación correctamente.";
            }

            return response;
        }

        public async Task<Response<CustomerDto>> GetByIdAsync(string id)
        {
            var response = new Response<CustomerDto>();

            var customer = await _unitOfWork.CustomersRepository.GetByIdAsync(id);
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

        public async Task<Response<bool>> InsertAsync(CustomerDto cutomersDto)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(cutomersDto);
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

        public async Task<Response<bool>> UpdateAsync(CustomerDto cutomersDto)
        {
            var response = new Response<bool>();

            var customer = _mapper.Map<Customer>(cutomersDto);
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
