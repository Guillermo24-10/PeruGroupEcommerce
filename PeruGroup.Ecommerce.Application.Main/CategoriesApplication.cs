using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;
using System.Text;
using System.Text.Json;

namespace PeruGroup.Ecommerce.Application.Main
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly ICategoriesDomain _categoriesDomain;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _distributedCache;

        public CategoriesApplication(ICategoriesDomain categoriesDomain, IMapper mapper, IDistributedCache distributedCache)
        {
            _categoriesDomain = categoriesDomain;
            _mapper = mapper;
            _distributedCache = distributedCache;
        }

        public async Task<Response<IEnumerable<CategoriesDto>>> GetAll()
        {
            var response = new Response<IEnumerable<CategoriesDto>>();
            var cacheKey = "categoriesList";

            try
            {
                var redisCategories = await _distributedCache.GetAsync(cacheKey);
                if (redisCategories != null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoriesDto>>(redisCategories);
                }
                else
                {
                    var categories = await _categoriesDomain.GetAll();
                    response.Data = _mapper.Map<IEnumerable<CategoriesDto>>(categories);
                    if (response.Data != null)
                    {
                        var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                        var options = new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(8), // tiempo de expiración absoluta de 8 horas
                            SlidingExpiration = TimeSpan.FromMinutes(60) // tiempo de expiración deslizante de 30 minutos
                        };

                        await _distributedCache.SetAsync(cacheKey, serializedCategories, options); // Guardar en caché
                    }
                }

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Se obtuvieron todas las categorías correctamente.";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
