using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Models;
using DivingCompetitionAPI.Data;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Score/create
        [HttpPost("create")]
        public ActionResult<Score> CreateScore([FromBody] Score score)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si ya existe un puntaje con la misma combinación de JudgeId, DiverId, DiveId y CompetitionId
            var existingScore = _context.Scores
                .FirstOrDefault(s => s.JudgeId == score.JudgeId &&
                                     s.DiverId == score.DiverId &&
                                     s.DiveId == score.DiveId &&
                                     s.CompetitionId == score.CompetitionId);

            if (existingScore != null)
            {
                return Conflict("Ya existe un puntaje para esta combinación de juez, clavadista, clavado y competencia.");
            }

            // Verify if the DiverDive exists
            var diverDive = _context.DiverDives
                .Include(dd => dd.Scores)
                .FirstOrDefault(dd => dd.DiverId == score.DiverId &&
                                      dd.DiveId == score.DiveId &&
                                      dd.CompetitionId == score.CompetitionId);

            if (diverDive == null)
            {
                return NotFound("DiverDive not found");
            }

            score.DiverDive = diverDive;

            _context.Scores.Add(score);
            _context.SaveChanges();

            return CreatedAtAction("GetScore", new { id = score.ScoreId }, score);
        }

        // GET: api/Score/{id}
        [HttpGet("{id}")]
        public ActionResult<Score> GetScore(int id)
        {
            var score = _context.Scores
                .Include(s => s.Judge)
                .Include(s => s.DiverDive)
                .FirstOrDefault(s => s.ScoreId == id);

            if (score == null)
            {
                return NotFound("Puntaje no encontrado");
            }

            return score;
        }
    }
}
