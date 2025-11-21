using PeruGroup.Ecommerce.Domain.Common;
using PeruGroup.Ecommerce.Domain.Entities;

namespace PeruGroup.Ecommerce.Domain.Specifications
{
    public class CountryInBlackListSpecification : ISpecification<Customer>
    {
        readonly List<string> _blackListedCountries =
            [
            "Argentina", "Brasil", "Chile", "Colombia", "Ecuador","Peru", "Venezuela","Uruguay", "Paraguay", "Bolivia"
            ];
        public bool IsSatisfiedBy(Customer entity)
        {
            return !_blackListedCountries.Contains(entity.Country ?? string.Empty);
        }
    }
}
