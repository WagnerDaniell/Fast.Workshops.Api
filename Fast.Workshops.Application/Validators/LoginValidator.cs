using Fast.Workshops.Application.DTOs.Auth;
using FluentValidation;

namespace Fast.Workshops.Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.")
                .MaximumLength(100);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo Password é obrigatório.")
                .MinimumLength(6).WithMessage("O campo Password deve conter no mínimo 6 caracteres.")
                .MaximumLength(100);
        }
    }
}
