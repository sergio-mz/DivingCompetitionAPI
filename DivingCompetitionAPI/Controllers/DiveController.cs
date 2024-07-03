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
    }
}

