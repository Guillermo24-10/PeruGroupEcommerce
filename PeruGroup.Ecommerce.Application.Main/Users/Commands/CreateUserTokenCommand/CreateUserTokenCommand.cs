using MediatR;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenCommand : IRequest<Response<UserDto>>
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
