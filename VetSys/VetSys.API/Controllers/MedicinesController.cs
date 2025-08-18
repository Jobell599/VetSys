using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;


namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineService _service;
        public MedicinesController(MedicineService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> GetAll() => Ok(await _service.GetAllMedicinesAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicineDto>> GetById(int id)
        {
            var medicine = await _service.GetMedicineByIdAsync(id);
            if (medicine == null) return NotFound();
            return Ok(medicine);
        }

        [HttpPost]
        public async Task<ActionResult<MedicineDto>> Create(MedicineDto dto)
        {
            var created = await _service.AddMedicineAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MedicineDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateMedicineAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteMedicineAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
