using Fast.Workshops.Application.DTOs.Auth;
using Fast.Workshops.Application.Services;
using Fast.Workshops.Application.Validators;
using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Auth
{
    public class RegisterUseCase
    {
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public RegisterUseCase(TokenService token, IUserRepository repository)
        {
            _tokenService = token;
            _userRepository = repository;
        }

        public async Task<AuthResponse> ExecuteRegister(RegisterRequest user)
        {
            var validation = new RegisterValidator();
            var resultValidation = validation.Validate(user);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser != null)
            {
                throw new ConflictException("Usuário já cadastrado!");
            }

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            };

            await _userRepository.CreateUserAsync(newUser);

            var accessToken = _tokenService.GenerateToken(newUser);

            return new AuthResponse
            {
                UserName = user.Name,
                AccessToken = accessToken,
            };

        }
    }
}
