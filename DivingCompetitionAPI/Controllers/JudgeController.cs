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
    }
}
