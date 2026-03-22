using Fast.Workshops.Application.DTOs.Workshops;
using FluentValidation;

namespace Fast.Workshops.Application.Validators
{
    public class WorkshopValidator : AbstractValidator<WorkshopRequest>
    {
        public WorkshopValidator()
        {
            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage("O nome do workshop é obrigatório.")
                 .MaximumLength(255).WithMessage("O nome do workshop deve ter no máximo 100 caracteres.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("A descrição do workshop deve ter no máximo 1000 caracteres.");
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("A data do workshop é obrigatório.");

        }
    }
}
