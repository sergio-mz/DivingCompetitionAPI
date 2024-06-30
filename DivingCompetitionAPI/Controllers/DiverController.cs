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
    }
}
