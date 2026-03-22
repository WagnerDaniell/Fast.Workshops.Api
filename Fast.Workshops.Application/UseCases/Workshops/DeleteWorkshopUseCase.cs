using Fast.Workshops.Domain.Repositories;
using Fast.Workshops.Domain.Exceptions;

namespace Fast.Workshops.Application.UseCases.Workshops
{
    public class DeleteWorkshopUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;

        public DeleteWorkshopUseCase(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task Execute(Guid id)
        {
            var workshop = await _workshopRepository.GetByIdAsync(id);
            if (workshop is null)
                throw new NotFoundException("Workshop não encontrado!");

            await _workshopRepository.DeleteAsync(id);
        }
    }
}