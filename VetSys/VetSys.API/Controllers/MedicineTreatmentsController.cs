using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineTreatmentsController : ControllerBase
    {
        private readonly MedicineTreatmentService _service;
        public MedicineTreatmentsController(MedicineTreatmentService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineTreatmentDto>>> GetAll() => Ok(await _service.GetAllMedicineTreatmentsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineTreatmentDto>> GetById(int id)
        {
            var mt = await _service.GetMedicineTreatmentByIdAsync(id);
            if (mt == null) return NotFound();
            return Ok(mt);
        }

        [HttpPost]
        public async Task<ActionResult<MedicineTreatmentDto>> Create(MedicineTreatmentDto dto)
        {
            var created = await _service.AddMedicineTreatmentAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteMedicineTreatmentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
