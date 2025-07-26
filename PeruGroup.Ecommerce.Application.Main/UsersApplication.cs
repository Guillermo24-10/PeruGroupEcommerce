using AutoMapper;
using PeruGroup.Ecommerce.Application.DTO;
using PeruGroup.Ecommerce.Application.Interface;
using PeruGroup.Ecommerce.Application.Validator;
using PeruGroup.Ecommerce.Domain.Interface;
using PeruGroup.Ecommerce.Transversal.Commons;

namespace PeruGroup.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersDomain _usersDomain;
        private readonly IMapper _mapper;
        private readonly UsersDtoValidator _userDtoValidator;

        public UsersApplication(IUsersDomain usersDomain, IMapper mapper, UsersDtoValidator userDtoValidator)
        {
            _usersDomain = usersDomain;
            _mapper = mapper;
            _userDtoValidator = userDtoValidator;
        }

        public async Task<Response<UsersDTO>> Authenticate(string username, string password)
        {
            var response = new Response<UsersDTO>();
            var validation = _userDtoValidator.Validate(
                            new UsersDTO { UserName = username, Password = password });

            if (!validation.IsValid)
            {
                response.Message = "Errores de Validacion";
                response.Errors = validation.Errors;
                response.IsSuccess = false;
                return response;
            }
            try
            {
                var user = await _usersDomain.Authenticate(username, password);
                response.Data = _mapper.Map<UsersDTO>(user);
                response.IsSuccess = true;
                response.Message = "Usuario autenticado correctamente.";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Usuario o contraseña invalidos.";
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
