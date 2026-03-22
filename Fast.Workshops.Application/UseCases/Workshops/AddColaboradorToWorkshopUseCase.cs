using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Workshops
{
    public class AddColaboradorToWorkshopUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IWorkshopColaboradorRepository _workshopColaboradorRepository;

        public AddColaboradorToWorkshopUseCase(
            IWorkshopRepository workshopRepository,
            IColaboradorRepository colaboradorRepository,
            IWorkshopColaboradorRepository workshopColaboradorRepository)
        {
            _workshopRepository = workshopRepository;
            _colaboradorRepository = colaboradorRepository;
            _workshopColaboradorRepository = workshopColaboradorRepository;
        }

        public async Task Execute(Guid workshopId, Guid colaboradorId)
        {
            var workshop = await _workshopRepository.GetByIdAsync(workshopId);
            if (workshop is null)
                throw new NotFoundException("Workshop não encontrado!");

            var colaborador = await _colaboradorRepository.GetByIdAsync(colaboradorId);
            if (colaborador is null)
                throw new NotFoundException("Colaborador não encontrado!");

            var existing = await _workshopColaboradorRepository.GetAsync(workshopId, colaboradorId);
            if (existing is not null)
                throw new ConflictException("Colaborador já está registrado nesse workshop!");

            var workshopColaborador = new WorkshopColaborador
            {
                WorkshopId = workshopId,
                ColaboradorId = colaboradorId
            };

            await _workshopColaboradorRepository.AddAsync(workshopColaborador);
        }
    }
}