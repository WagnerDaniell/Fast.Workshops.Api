using Fast.Workshops.Application.DTOs.Colaboradores;
using FluentValidation;

namespace Fast.Workshops.Application.Validators
{
    public class ColaboradorValidator : AbstractValidator<ColaboradorRequest>
    {
        public ColaboradorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do colaborador é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do colaborador deve ter no máximo 100 caracteres.");
        }
    }
}