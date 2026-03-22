using Fast.Workshops.Application.DTOs.Colaboradores;
using Fast.Workshops.Application.DTOs.Workshops;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Workshops
{
    public class ReadWorkshopUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;

        public ReadWorkshopUseCase(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }
        public async Task<List<WorkshopResponse>> ExecuteGetAll()
        {
            var workshops = await _workshopRepository.GetAllAsync();
            return workshops.Select(w => new WorkshopResponse
            {
                Id = w.Id,
                Name = w.Name,
                Date = w.Date,
                Description = w.Description,
                CreatedAt = w.CreatedAt 
            }).ToList();
        }

        public async Task<WorkshopResponse> ExecuteGetById(Guid id)
        {
            var workshop = await _workshopRepository.GetByIdAsync(id);
            if (workshop is null)
                throw new NotFoundException("Workshop não encontrado!");

            return new WorkshopResponse
            {
                Id = workshop.Id,
                Name = workshop.Name,
                Date = workshop.Date,
                Description = workshop.Description,
                CreatedAt = workshop.CreatedAt,
                Colaboradores = workshop.WorkshopColaboradores?
                    .Where(wc => wc.Colaborador is not null)
                    .Select(wc => new ColaboradorResponse
                    {
                        Id = wc.Colaborador!.Id,
                        Name = wc.Colaborador.Name
                    }).ToList() ?? new()
            };
        }
    }
}
