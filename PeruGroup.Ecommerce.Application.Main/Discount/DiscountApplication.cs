using AutoMapper;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Interface.Infrastructure;
using PeruGroup.Ecommerce.Application.Interface.UseCases;
using PeruGroup.Ecommerce.Domain.Events;
using PeruGroup.Ecommerce.Transversal.Commons;
using System.Text.Json;
using Disco = PeruGroup.Ecommerce.Domain.Entities.Discount;

namespace PeruGroup.Ecommerce.Application.UseCases.Discount
{
    public class DiscountApplication : IDiscountApplication
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly IEventBus _eventBus;
        public readonly INotification _notification;

        public DiscountApplication(IUnitOfWork unitOfWork, IMapper mapper, IEventBus eventBus, INotification notification)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventBus = eventBus;
            _notification = notification;
        }

        public async Task<Response<bool>> Create(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var discount = _mapper.Map<Disco>(discountDto);
                await _unitOfWork.DiscountRepository.InsertAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se registro correctamente";

                    /* publicamos el evento */
                    var discountCreatedEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                    _eventBus.Publish(discountCreatedEvent);

                    /* Enviamos notificación correo*/
                    await _notification.SendEmailAsync(response.Message, JsonSerializer.Serialize(discount), cancellationToken);
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                await _notification.SendEmailAsync(response.Message, JsonSerializer.Serialize(response), cancellationToken);
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                await _unitOfWork.DiscountRepository.DeleteAsync(id.ToString());
                response.Data = await _unitOfWork.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se eliminó correctamente";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                throw;
            }

            return response;
        }

        public async Task<Response<DiscountDto>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDto>();

            try
            {
                var discount = await _unitOfWork.DiscountRepository.GetAsync(id, cancellationToken);
                if (discount is null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento no existe";
                    return response;
                }

                response.Data = _mapper.Map<DiscountDto>(discount);
                response.IsSuccess = true;
                response.Message = "Se obtuvo Discount correctamente";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<DiscountDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<List<DiscountDto>>();
            try
            {
                var discounts = await _unitOfWork.DiscountRepository.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<List<DiscountDto>>(discounts);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Se obtuvieron Discounts correctamente";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<DiscountDto>>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<DiscountDto>>();
            try
            {
                var count = await _unitOfWork.DiscountRepository.CountAsync();
                var discount = await _unitOfWork.DiscountRepository.GetAllWithPaginationAsync(pageNumber, pageSize);

                response.Data = _mapper.Map<IEnumerable<DiscountDto>>(discount);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Se obtuvieron Discounts correctamente";
                    response.PageNumber = pageNumber;
                    response.TotalCount = count;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> Update(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var discount = _mapper.Map<Disco>(discountDto);
                await _unitOfWork.DiscountRepository.UpdateAsync(discount);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se actualizó correctamente";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
