using Fast.Workshops.Application.DTOs.Colaboradores;
using Fast.Workshops.Application.UseCases.Colaboradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fast.Workshops.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/colaboradores")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly CreateColaboradorUseCase _createColaboradorUseCase;
        private readonly ReadColaboradorUseCase _readColaboradorUseCase;
        private readonly UpdateColaboradorUseCase _updateColaboradorUseCase;
        private readonly DeleteColaboradorUseCase _deleteColaboradorUseCase;

        public ColaboradoresController(
            CreateColaboradorUseCase createColaboradorUseCase,
            ReadColaboradorUseCase readColaboradorUseCase,
            UpdateColaboradorUseCase updateColaboradorUseCase,
            DeleteColaboradorUseCase deleteColaboradorUseCase)
        {
            _createColaboradorUseCase = createColaboradorUseCase;
            _readColaboradorUseCase = readColaboradorUseCase;
            _updateColaboradorUseCase = updateColaboradorUseCase;
            _deleteColaboradorUseCase = deleteColaboradorUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ColaboradorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _readColaboradorUseCase.ExecuteGetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ColaboradorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _readColaboradorUseCase.ExecuteGetById(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ColaboradorResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ColaboradorRequest request)
        {
            var result = await _createColaboradorUseCase.Execute(request);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ColaboradorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ColaboradorRequest request)
        {
            var result = await _updateColaboradorUseCase.Execute(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteColaboradorUseCase.Execute(id);
            return NoContent();
        }
    }
}