using FluentValidation;
using Fast.Workshops.Application.DTOs.Auth;

namespace Fast.Workshops.Application.Validators
{
    public class RegisterValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .EmailAddress().WithMessage("O campo Email deve ser um endereço de email válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("O campo Password é obrigatório.")
                .MinimumLength(6).WithMessage("O campo Password deve conter no mínimo 6 caracteres.");
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("O campo Name é obrigatório.")
               .MinimumLength(2).WithMessage("O campo Name deve conter no mínimo 2 caracteres.");
        }
    }
}
