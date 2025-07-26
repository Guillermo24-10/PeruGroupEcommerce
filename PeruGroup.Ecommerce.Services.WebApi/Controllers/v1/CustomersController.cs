using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;

namespace PeruGroup.Ecommerce.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CustomersController : Controller
    {
        private readonly ICustomersApplication _customersApplication;

        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        [HttpPost("InsertAsync")]
        public async Task<IActionResult> InsertAsync([FromBody] CutomersDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = await _customersApplication.InsertAsync(customersDto);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] CutomersDto customersDto)
        {
            if (customersDto == null)
                return BadRequest();

            var response = await _customersApplication.UpdateAsync(customersDto);
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
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var response = await _customersApplication.DeleteAsync(id);
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

            var response = await _customersApplication.GetByIdAsync(id);
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
            var response = await _customersApplication.GetAllAsync();
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
