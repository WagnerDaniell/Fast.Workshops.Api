using Fast.Workshops.Application.DTOs.Colaboradores;
using Fast.Workshops.Application.Validators;
using Fast.Workshops.Domain.Entities;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Colaboradores
{
    public class CreateColaboradorUseCase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public CreateColaboradorUseCase(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<ColaboradorResponse> Execute(ColaboradorRequest request)
        {
            var validator = new ColaboradorValidator();
            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var colaborador = new Colaborador
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await _colaboradorRepository.CreateAsync(colaborador);

            return new ColaboradorResponse
            {
                Id = colaborador.Id,
                Name = colaborador.Name
            };
        }
    }
}