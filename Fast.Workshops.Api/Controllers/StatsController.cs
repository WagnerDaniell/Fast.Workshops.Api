using Fast.Workshops.Application.DTOs.Stats;
using Fast.Workshops.Application.UseCases.Stats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fast.Workshops.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/stats")]
    public class StatsController : ControllerBase
    {
        private readonly ReadStatsUseCase _readStatsUseCase;

        public StatsController(ReadStatsUseCase readStatsUseCase)
        {
            _readStatsUseCase = readStatsUseCase;
        }

        [HttpGet("colaboradores-participacao")]
        [ProducesResponseType(typeof(List<ColaboradorStatsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetColaboradoresParticipacao()
        {
            var result = await _readStatsUseCase.ExecuteColaboradoresParticipacao();
            return Ok(result);
        }

        [HttpGet("workshops-participacao")]
        [ProducesResponseType(typeof(List<WorkshopStatsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWorkshopsParticipacao()
        {
            var result = await _readStatsUseCase.ExecuteWorkshopsParticipacao();
            return Ok(result);
        }
    }
}