using Fast.Workshops.Application.DTOs.Workshops;
using Fast.Workshops.Application.Validators;
using Fast.Workshops.Domain.Repositories;
using Fast.Workshops.Domain.Exceptions;

namespace Fast.Workshops.Application.UseCases.Workshops
{
    public class UpdateWorkshopUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;

        public UpdateWorkshopUseCase(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task<WorkshopResponse> Execute(Guid id, WorkshopRequest request)
        {
            var validator = new WorkshopValidator();
            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var workshop = await _workshopRepository.GetByIdAsync(id);
            if (workshop is null)
                throw new NotFoundException("Workshop não encontrado!");

            var existingWorkshop = await _workshopRepository.GetByDateAsync(request.Date);
            if (existingWorkshop != null && existingWorkshop.Id != id)
                throw new ConflictException("Já existe um workshop agendado para essa data e horário!");

            workshop.Name = request.Name;
            workshop.Date = request.Date;
            workshop.Description = request.Description;

            await _workshopRepository.UpdateAsync(workshop);

            return new WorkshopResponse
            {
                Id = workshop.Id,
                Name = workshop.Name,
                Date = workshop.Date,
                Description = workshop.Description,
                CreatedAt = workshop.CreatedAt
            };
        }
    }
}