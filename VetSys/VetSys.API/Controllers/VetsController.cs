using Microsoft.AspNetCore.Mvc;
using VetSys.Application.Dtos;

namespace VetSys.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VetsController : ControllerBase
    {
        private readonly VetService _service;
        public VetsController(VetService service) => _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetDto>>> GetAll() => Ok(await _service.GetAllVetsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<VetDto>> GetById(int id)
        {
            var vet = await _service.GetVetByIdAsync(id);
            if (vet == null) return NotFound();
            return Ok(vet);
        }

        [HttpPost]
        public async Task<ActionResult<VetDto>> Create(VetDto dto)
        {
            var created = await _service.AddVetAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VetDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _service.UpdateVetAsync(dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteVetAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

