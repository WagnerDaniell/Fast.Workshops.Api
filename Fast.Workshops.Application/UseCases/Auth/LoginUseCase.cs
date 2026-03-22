using Fast.Workshops.Application.Services;
using Fast.Workshops.Domain.Repositories;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Application.DTOs.Auth;
using Fast.Workshops.Application.Validators;

namespace Fast.Workshops.Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public LoginUseCase(TokenService token, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenService = token;
        }

        public async Task<AuthResponse> ExecuteLogin(LoginRequest login)
        {
            var validator = new LoginValidator();
            var resultValidation = validator.Validate(login);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var user = await _userRepository.GetByEmailAsync(login.Email);

            if (user == null)
            {
                throw new NotFoundException("Usuario não encontrado!");
            }

            if (!BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            {
                throw new UnauthorizedException("Senha incorreta!");
            }

            var accessToken = _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                UserName = user.Name,
                AccessToken = accessToken,
            };
        }
    }
}
