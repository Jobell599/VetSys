using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalHistoriesController : ControllerBase
    {
        private readonly MedicalHistoryService _service;
        public MedicalHistoriesController(MedicalHistoryService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalHistoryDto>>> GetAll() => Ok(await _service.GetAllMedicalHistoriesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalHistoryDto>> GetById(int id)
        {
            var history = await _service.GetMedicalHistoryByIdAsync(id);
            if (history == null) return NotFound();
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalHistoryDto>> Create(MedicalHistoryDto dto)
        {
            var created = await _service.AddMedicalHistoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteMedicalHistoryAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
