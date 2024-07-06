using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Models;
using System;
using System.Linq;
using DivingCompetitionAPI.Data;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Dive/create
        [HttpPost("create")]
        public ActionResult<Dive> CreateDive([FromBody] Dive dive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el Dive ya existe
            var existingDive = _context.Dives
                .FirstOrDefault(d => d.DiveCode == dive.DiveCode && d.Group == dive.Group && d.Height == dive.Height);

            if (existingDive != null)
            {
                return Conflict("Ya existe un Dive con el mismo DiveCode, Group y Height");
            }

            _context.Dives.Add(dive);
            _context.SaveChanges();

            return CreatedAtAction("GetDive", new { id = dive.DiveId }, dive);
        }

        // GET: api/Dive/{id}
        [HttpGet("{id}")]
        public ActionResult<Dive> GetDive(int id)
        {
            var dive = _context.Dives.Find(id);

            if (dive == null)
            {
                return NotFound("Clavado no encontrado");
            }

            return dive;
        }

        // PUT: api/Dive/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDive(int id, [FromBody] Dive dive)
        {
            if (id != dive.DiveId)
            {
                return BadRequest("ID de clavado no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDive = _context.Dives.AsNoTracking()
                .FirstOrDefault(d => d.DiveId == id);

            if (existingDive == null)
            {
                return NotFound("Clavado no encontrado");
            }

            // Verificar si el Dive ya existe con las mismas propiedades
            var duplicateDive = _context.Dives
                .FirstOrDefault(d => d.DiveId != id && d.DiveCode == dive.DiveCode && d.Group == dive.Group && d.Height == dive.Height);

            if (duplicateDive != null)
            {
                return Conflict("Ya existe un Dive con el mismo DiveCode, Group y Height");
            }

            _context.Entry(dive).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Dives.Any(d => d.DiveId == id))
                {
                    return NotFound("Clavado no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Dive/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDive(int id)
        {
            var dive = _context.Dives.Find(id);

            if (dive == null)
            {
                return NotFound("Clavado no encontrado");
            }

            _context.Dives.Remove(dive);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
