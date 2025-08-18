using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultationsController : ControllerBase
    {
        private readonly ConsultationService _service;
        public ConsultationsController(ConsultationService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationDto>>> GetAll() => Ok(await _service.GetAllConsultationsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationDto>> GetById(int id)
        {
            var consultation = await _service.GetConsultationByIdAsync(id);
            if (consultation == null) return NotFound();
            return Ok(consultation);
        }

        [HttpPost]
        public async Task<ActionResult<ConsultationDto>> Create(ConsultationDto dto)
        {
            var created = await _service.AddConsultationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ConsultationDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateConsultationAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteConsultationAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
