using LawFirmApp.Models;
using LawFirmApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LawFirmApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttorneysController : ControllerBase
    {
        private readonly IAttorneyRepository _repository;

        public AttorneysController(IAttorneyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attorney>>> GetAll()
        {
            var attorneys = await _repository.GetAllAsync();
            return Ok(attorneys);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attorney>> GetById(int id)
        {
            var attorney = await _repository.GetByIdAsync(id);
            if (attorney == null)
                return NotFound($"Attorney with ID {id} not found.");

            return Ok(attorney);
        }

        [HttpPost]
        public async Task<ActionResult<Attorney>> AddAttorney([FromBody] Attorney attorney)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdAttorney = await _repository.AddAsync(attorney);
            return CreatedAtAction(nameof(GetById), new { id = createdAttorney.Id }, createdAttorney);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Attorney attorney)
        {
            if (id != attorney.Id)
                return BadRequest("Attorney ID in the URL does not match the ID in the body.");

            var updatedAttorney = await _repository.UpdateAsync(attorney);
            if (updatedAttorney == null)
                return NotFound($"Attorney with ID {id} not found.");

            return Ok(updatedAttorney);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repository.DeleteAsync(id);
            if (!success)
                return NotFound($"Attorney with ID {id} not found.");

            return NoContent();
        }
    }
}
