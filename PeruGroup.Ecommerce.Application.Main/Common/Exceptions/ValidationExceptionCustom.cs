using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Common.Exceptions
{
    public class ValidationExceptionCustom : Exception
    {
        public ValidationExceptionCustom() : base("One or more validation failures have occurred.")
        {
            Errors = new List<BaseError>();
        }
        public IEnumerable<BaseError> Errors { get; }

        public ValidationExceptionCustom(IEnumerable<BaseError> errors) : this()
        {
            Errors = errors;
        }
    }
}
