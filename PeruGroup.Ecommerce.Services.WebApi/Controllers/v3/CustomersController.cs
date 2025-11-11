using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;
using PeruGroup.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;

namespace PeruGroup.Ecommerce.Services.WebApi.Controllers.v3
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CreateCustomerCommand command)
        {
            if (command == null)
                return BadRequest();

            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("UpdateAsync/{customerId}")]
        public async Task<IActionResult> UpdateAsync(string customerId, [FromBody] UpdateCustomerCommand customersDto)
        {
            var customerDto = await _mediator.Send(new GetCustomerQuery { CustomerId = customerId });
            if (customerDto.Data == null)
                return NotFound(customerDto!.Message);

            if (customersDto == null)
                return BadRequest();

            var response = await _mediator.Send(customersDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(DeleteCustomerCommand command)
        {
            if (string.IsNullOrEmpty(command.CustomerId))
                return BadRequest();

            var response = await _mediator.Send(new DeleteCustomerCommand { CustomerId = command.CustomerId });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var response = await _mediator.Send(new GetCustomerQuery { CustomerId = id });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("GetAllWithPaginationAync")]
        public async Task<IActionResult> GetAllWithPaginationAync([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(new GetAllWithPaginationCustomerQuery { PageNumber = pageNumber, PageSize = pageSize });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
