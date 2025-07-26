using AutoMapper;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Entity;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Main
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly ICustomersDomain _costumersDomain;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CustomersApplication> _logger;

        public CustomersApplication(ICustomersDomain costumersDomain, IMapper mapper, IAppLogger<CustomersApplication> logger)
        {
            _costumersDomain = costumersDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> DeleteAsync(string id)
        {
            var response = new Response<bool>();
            try
            {
                var result = await _costumersDomain.DeleteAsync(id);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo eliminar el customer. Verifique si el ID es correcto.";
                    return response;
                }
                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Se eliminó customer correctamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al eliminar customer: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<IEnumerable<CutomersDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CutomersDto>>();
            try
            {
                var customers = await _costumersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CutomersDto>>(customers);
                response.IsSuccess = true;
                response.Message = "Se obtuvieron todos los customers correctamente.";
                _logger.LogInformation("Se obtuvieron todos los customers correctamente.");
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al obtener customers: {ex.Message}";
                _logger.LogError("Error al obtener customers: {Message}", ex.Message);
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<CutomersDto>>> GetAllPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<CutomersDto>>();

            try
            {
                var count = await _costumersDomain.CountAsync();
                var customers = await _costumersDomain.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<CutomersDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Se obtuvieron los customers con paginación correctamente.";
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Error al obtener customers con paginación: {ex.Message}";
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<Response<CutomersDto>> GetByIdAsync(string id)
        {
            var response = new Response<CutomersDto>();

            try
            {
                var customer = await _costumersDomain.GetByIdAsync(id);
                if (customer == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Customer no encontrado.";
                    return response;
                }
                response.Data = _mapper.Map<CutomersDto>(customer);
                response.IsSuccess = true;
                response.Message = "Se obtuvo customer correctamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al obtener customer: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> InsertAsync(CutomersDto cutomersDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(cutomersDto);
                var result = await _costumersDomain.InsertAsync(customer);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo insertar el customer. Verifique los datos.";
                    return response;
                }
                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Se insertó customer correctamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al insertar customer: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CutomersDto cutomersDto)
        {
            var response = new Response<bool>();

            try
            {
                var customer = _mapper.Map<Customers>(cutomersDto);
                var result = await _costumersDomain.UpdateAsync(customer);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = "No se pudo actualizar el customer. Verifique los datos.";
                    return response;
                }
                response.Data = result;
                response.IsSuccess = true;
                response.Message = "Se actualizó customer correctamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al actualizar customer: {ex.Message}";
            }

            return response;
        }
    }
}
