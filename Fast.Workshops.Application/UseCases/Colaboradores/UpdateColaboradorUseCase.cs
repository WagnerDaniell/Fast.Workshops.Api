using Fast.Workshops.Application.DTOs.Colaboradores;
using Fast.Workshops.Application.Validators;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Colaboradores
{
    public class UpdateColaboradorUseCase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public UpdateColaboradorUseCase(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<ColaboradorResponse> Execute(Guid id, ColaboradorRequest request)
        {
            var validator = new ColaboradorValidator();
            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var colaborador = await _colaboradorRepository.GetByIdAsync(id);
            if (colaborador is null)
                throw new NotFoundException("Colaborador não encontrado!");

            colaborador.Name = request.Name;

            await _colaboradorRepository.UpdateAsync(colaborador);

            return new ColaboradorResponse
            {
                Id = colaborador.Id,
                Name = colaborador.Name
            };
        }
    }
}