using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Colaboradores
{
    public class DeleteColaboradorUseCase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public DeleteColaboradorUseCase(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task Execute(Guid id)
        {
            var colaborador = await _colaboradorRepository.GetByIdAsync(id);
            if (colaborador is null)
                throw new NotFoundException("Colaborador não encontrado!");

            await _colaboradorRepository.DeleteAsync(id);
        }
    }
}