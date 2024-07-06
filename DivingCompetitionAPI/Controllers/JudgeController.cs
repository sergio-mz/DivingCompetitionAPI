using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Data;
using DivingCompetitionAPI.Models;
using System.Linq;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JudgeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Judge/{competitionId}
        [HttpPost("{competitionId}")]
        public ActionResult<Judge> AddJudgeToCompetition(int competitionId, [FromBody] Judge judge)
        {
            var competition = _context.Competitions.Find(competitionId);

            if (competition == null)
            {
                return NotFound("Competición no encontrada");
            }

            _context.Judges.Add(judge);
            _context.SaveChanges();

            // Asociar el juez a la competición
            var competitionJudge = new CompetitionJudge
            {
                CompetitionId = competitionId,
                JudgeId = judge.JudgeId
            };

            _context.CompetitionJudges.Add(competitionJudge);
            _context.SaveChanges();

            return CreatedAtAction("GetJudge", new { id = judge.JudgeId }, judge);
        }

        // GET: api/Judge/{id}
        [HttpGet("{id}")]
        public ActionResult<Judge> GetJudge(int id)
        {
            var judge = _context.Judges.Find(id);

            if (judge == null)
            {
                return NotFound();
            }

            return judge;
        }

        // PUT: api/Judge/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateJudge(int id, [FromBody] Judge judge)
        {
            if (id != judge.JudgeId)
            {
                return BadRequest("El ID del juez en el cuerpo no coincide con el ID en la URL");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(judge).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Judges.Any(j => j.JudgeId == id))
                {
                    return NotFound("Juez no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Judge/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteJudge(int id)
        {
            var judge = _context.Judges.Find(id);

            if (judge == null)
            {
                return NotFound("Juez no encontrado");
            }

            // Eliminar asociaciones del juez con competiciones y puntuaciones
            var competitionJudges = _context.CompetitionJudges.Where(cj => cj.JudgeId == id).ToList();
            var scores = _context.Scores.Where(s => s.JudgeId == id).ToList();

            _context.CompetitionJudges.RemoveRange(competitionJudges);
            _context.Scores.RemoveRange(scores);
            _context.Judges.Remove(judge);

            _context.SaveChanges();

            return NoContent();
        }
    }
}
