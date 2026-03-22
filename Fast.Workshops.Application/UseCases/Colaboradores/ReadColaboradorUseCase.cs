using Fast.Workshops.Application.DTOs.Colaboradores;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Colaboradores
{
    public class ReadColaboradorUseCase
    {
        private readonly IColaboradorRepository _colaboradorRepository;

        public ReadColaboradorUseCase(IColaboradorRepository colaboradorRepository)
        {
            _colaboradorRepository = colaboradorRepository;
        }

        public async Task<List<ColaboradorResponse>> ExecuteGetAll()
        {
            var colaboradores = await _colaboradorRepository.GetAllAsync();

            return colaboradores.Select(c => new ColaboradorResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public async Task<ColaboradorResponse> ExecuteGetById(Guid id)
        {
            var colaborador = await _colaboradorRepository.GetByIdAsync(id);
            if (colaborador is null)
                throw new NotFoundException("Colaborador não encontrado!");

            return new ColaboradorResponse
            {
                Id = colaborador.Id,
                Name = colaborador.Name
            };
        }
    }
}