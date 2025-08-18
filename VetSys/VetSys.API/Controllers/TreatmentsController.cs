using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TreatmentsController : ControllerBase
    {
        private readonly TreatmentService _service;
        public TreatmentsController(TreatmentService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TreatmentDto>>> GetAll() => Ok(await _service.GetAllTreatmentsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TreatmentDto>> GetById(int id)
        {
            var treatment = await _service.GetTreatmentByIdAsync(id);
            if (treatment == null) return NotFound();
            return Ok(treatment);
        }

        [HttpPost]
        public async Task<ActionResult<TreatmentDto>> Create(TreatmentDto dto)
        {
            var created = await _service.AddTreatmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TreatmentDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateTreatmentAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteTreatmentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

