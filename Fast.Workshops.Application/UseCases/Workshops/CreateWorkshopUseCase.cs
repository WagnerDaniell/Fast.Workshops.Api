using Fast.Workshops.Application.DTOs.Workshops;
using Fast.Workshops.Application.Validators;
using Fast.Workshops.Domain.Repositories;
using Fast.Workshops.Domain.Exceptions;
using Fast.Workshops.Domain.Entities;

namespace Fast.Workshops.Application.UseCases.Workshops
{
    public class CreateWorkshopUseCase
    {
        private readonly IWorkshopRepository _workshopRepository;
        public CreateWorkshopUseCase(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task<WorkshopResponse> Execute(WorkshopRequest request)
        {
            var validator = new WorkshopValidator();
            var resultValidation = validator.Validate(request);

            if (!resultValidation.IsValid)
            {
                var errors = resultValidation.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errors);
            }

            var existingWorkshop = await _workshopRepository.GetByDateAsync(request.Date);
            if (existingWorkshop != null)
                throw new ConflictException("Já existe um workshop agendado para essa data e horário!");

            var workshop = new Workshop
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Date = request.Date,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _workshopRepository.CreateAsync(workshop);

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
