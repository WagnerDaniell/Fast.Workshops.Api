using Fast.Workshops.Application.DTOs.Stats;
using Fast.Workshops.Domain.Repositories;

namespace Fast.Workshops.Application.UseCases.Stats
{
    public class ReadStatsUseCase
    {
        private readonly IStatsRepository _statsRepository;

        public ReadStatsUseCase(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<List<ColaboradorStatsResponse>> ExecuteColaboradoresParticipacao()
        {
            var colaboradores = await _statsRepository.GetColaboradoresComWorkshopsAsync();
            return colaboradores
                .Select(c => new ColaboradorStatsResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    TotalWorkshops = c.WorkshopColaboradores?.Count ?? 0
                })
                .OrderByDescending(c => c.TotalWorkshops)
                .ToList();
        }

        public async Task<List<WorkshopStatsResponse>> ExecuteWorkshopsParticipacao()
        {
            var workshops = await _statsRepository.GetWorkshopsComColaboradoresAsync();
            return workshops
                .Select(w => new WorkshopStatsResponse
                {
                    Id = w.Id,
                    Name = w.Name,
                    TotalColaboradores = w.WorkshopColaboradores?.Count ?? 0
                })
                .OrderByDescending(w => w.TotalColaboradores)
                .ToList();
        }
    }
}
