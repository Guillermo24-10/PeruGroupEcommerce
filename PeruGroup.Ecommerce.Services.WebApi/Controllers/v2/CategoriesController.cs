using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface.UseCases;
using PeruGroup.Ecommerce.Transversal.Commons;
using Swashbuckle.AspNetCore.Annotations;

namespace PeruGroup.Ecommerce.Services.WebApi.Controllers.v2
{
    [Authorize]
    [EnableRateLimiting(policyName: "FixedWindow")] // Aplica la política de limitación de tasa "FixedWindowPolicy" a todas las acciones del controlador
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [SwaggerTag("Obtener Categorías de Productos")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesApplication _categoriesApplication;

        public CategoriesController(ICategoriesApplication categoriesApplication)
        {
            _categoriesApplication = categoriesApplication;
        }

        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "Obtener todas las categorías", Description = "Obtiene todas las categorías de productos", OperationId = "GetAll", Tags = new[] { "GetAll" })]
        [SwaggerResponse(200, "Lista de categorias", typeof(Response<IEnumerable<CategoryDto>>))]
        [SwaggerResponse(404, "No se encontraron categorías ")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoriesApplication.GetAll();
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
