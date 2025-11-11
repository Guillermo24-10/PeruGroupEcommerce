using AutoMapper;
using MediatR;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenCommandHandler : IRequestHandler<CreateUserTokenCommand, Response<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserTokenCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserDto>();
            var user = await _unitOfWork.UsersRepository.Authenticate(request.username!, request.password!);
            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = "Usuario no existe";
                return response;
            }

            response.Data = _mapper.Map<UserDto>(user);
            response.IsSuccess = true;
            response.Message = "Usuario autenticado correctamente.";
            return response;
        }
    }
}
