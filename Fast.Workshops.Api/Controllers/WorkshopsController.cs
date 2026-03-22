using Fast.Workshops.Application.DTOs.Workshops;
using Fast.Workshops.Application.UseCases.Workshops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fast.Workshops.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/workshops")]
    public class WorkshopsController : ControllerBase
    {
        private readonly CreateWorkshopUseCase _createWorkshopUseCase;
        private readonly ReadWorkshopUseCase _readWorkshopUseCase;
        private readonly UpdateWorkshopUseCase _updateWorkshopUseCase;
        private readonly DeleteWorkshopUseCase _deleteWorkshopUseCase;
        private readonly AddColaboradorToWorkshopUseCase _addColaboradorToWorkshopUseCase;
        private readonly RemoveColaboradorFromWorkshopUseCase _removeColaboradorFromWorkshopUseCase;

        public WorkshopsController(
            CreateWorkshopUseCase createWorkshopUseCase,
            ReadWorkshopUseCase readWorkshopUseCase,
            UpdateWorkshopUseCase updateWorkshopUseCase,
            DeleteWorkshopUseCase deleteWorkshopUseCase,
            AddColaboradorToWorkshopUseCase addColaboradorToWorkshopUseCase,
            RemoveColaboradorFromWorkshopUseCase removeColaboradorFromWorkshopUseCase)
        {
            _createWorkshopUseCase = createWorkshopUseCase;
            _readWorkshopUseCase = readWorkshopUseCase;
            _updateWorkshopUseCase = updateWorkshopUseCase;
            _deleteWorkshopUseCase = deleteWorkshopUseCase;
            _addColaboradorToWorkshopUseCase = addColaboradorToWorkshopUseCase;
            _removeColaboradorFromWorkshopUseCase = removeColaboradorFromWorkshopUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<WorkshopResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _readWorkshopUseCase.ExecuteGetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _readWorkshopUseCase.ExecuteGetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] WorkshopRequest request)
        {
            var result = await _createWorkshopUseCase.Execute(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(WorkshopResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] WorkshopRequest request)
        {
            var result = await _updateWorkshopUseCase.Execute(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteWorkshopUseCase.Execute(id);
            return NoContent();
        }

        [HttpPost("{id}/colaboradores/{colaboradorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddColaborador([FromRoute] Guid id, [FromRoute] Guid colaboradorId)
        {
            await _addColaboradorToWorkshopUseCase.Execute(id, colaboradorId);
            return NoContent();
        }

        [HttpDelete("{id}/colaboradores/{colaboradorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveColaborador([FromRoute] Guid id, [FromRoute] Guid colaboradorId)
        {
            await _removeColaboradorFromWorkshopUseCase.Execute(id, colaboradorId);
            return NoContent();
        }
    }
}