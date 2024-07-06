using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Data;
using DivingCompetitionAPI.Models;
using System.Linq;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiverController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiverController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Diver/{competitionId}
        [HttpPost("{competitionId}")]
        public ActionResult<Diver> AddDiverToCompetition(int competitionId, [FromBody] Diver diver)
        {
            var competition = _context.Competitions.Find(competitionId);

            if (competition == null)
            {
                return NotFound("Competición no encontrada");
            }

            _context.Divers.Add(diver);
            _context.SaveChanges();

            // Asociar el clavadista a la competición
            var competitionDiver = new CompetitionDiver
            {
                CompetitionId = competitionId,
                DiverId = diver.DiverId
            };

            _context.CompetitionDivers.Add(competitionDiver);
            _context.SaveChanges();

            return CreatedAtAction("GetDiver", new { id = diver.DiverId }, diver);
        }

        // GET: api/Diver/{id}
        [HttpGet("{id}")]
        public ActionResult<Diver> GetDiver(int id)
        {
            var diver = _context.Divers.Find(id);

            if (diver == null)
            {
                return NotFound();
            }

            return diver;
        }

        // PUT: api/Diver/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateDiver(int id, [FromBody] Diver diver)
        {
            if (id != diver.DiverId)
            {
                return BadRequest("El ID del clavadista en el cuerpo no coincide con el ID en la URL");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(diver).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Divers.Any(d => d.DiverId == id))
                {
                    return NotFound("Clavadista no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Diver/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteDiver(int id)
        {
            var diver = _context.Divers.Find(id);

            if (diver == null)
            {
                return NotFound("Clavadista no encontrado");
            }

            // Eliminar asociaciones del clavadista con competiciones y clavados
            var competitionDivers = _context.CompetitionDivers.Where(cd => cd.DiverId == id).ToList();
            var diverDives = _context.DiverDives.Where(dd => dd.DiverId == id).ToList();
            var scores = _context.Scores.Where(s => s.DiverId == id).ToList();

            _context.CompetitionDivers.RemoveRange(competitionDivers);
            _context.DiverDives.RemoveRange(diverDives);
            _context.Scores.RemoveRange(scores);
            _context.Divers.Remove(diver);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
