using Microsoft.AspNetCore.Mvc;
using DivingCompetitionAPI.Models;
using DivingCompetitionAPI.Data;
using System.Linq;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiverDiveController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiverDiveController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/DiverDive/add
        [HttpPost("add")]
        public ActionResult AddDiveToDiver([FromBody] DiverDive diverDive)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var diver = _context.Divers.Find(diverDive.DiverId);
            if (diver == null)
            {
                return NotFound("Clavadista no encontrado");
            }

            var dive = _context.Dives.Find(diverDive.DiveId);
            if (dive == null)
            {
                return NotFound("Clavado no encontrado");
            }

            var competition = _context.Competitions.Find(diverDive.CompetitionId);
            if (competition == null)
            {
                return NotFound("Competencia no encontrada");
            }

            // Verificar si ya existe un registro con las mismas IDs
            var existingDiverDive = _context.DiverDives
                .FirstOrDefault(dd => dd.DiverId == diverDive.DiverId && dd.DiveId == diverDive.DiveId && dd.CompetitionId == diverDive.CompetitionId);

            if (existingDiverDive != null)
            {
                return Conflict("El clavadista ya tiene este clavado registrado en esta competencia.");
            }

            _context.DiverDives.Add(diverDive);
            _context.SaveChanges();

            return Ok("Clavado agregado al clavadista en la competencia exitosamente.");
        }
    }
}
