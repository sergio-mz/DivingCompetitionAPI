using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DivingCompetitionAPI.Data;
using DivingCompetitionAPI.Models;
using System;
using System.Linq;

namespace DivingCompetitionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompetitionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Competition/create
        [HttpPost("create")]
        public ActionResult<Competition> CreateCompetition([FromBody] Competition competition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Generar un código único para la competición
                string uniqueCode = GenerateUniqueCode();

                // Asignar el código único y guardar la competición
                competition.Code = uniqueCode;

                _context.Competitions.Add(competition);
                _context.SaveChanges();

                // Retorna un código 201 Created y los detalles de la competición creada
                return CreatedAtAction("GetCompetition", new { id = competition.CompetitionId }, competition);
            }
            catch (Exception ex)
            {
                // En caso de error al guardar la competición, retorna un error 500 Internal Server Error
                return StatusCode(500, $"Error interno al crear la competición: {ex.Message}");
            }
        }

        // GET: api/Competition/{id}
        [HttpGet("{id}")]
        public ActionResult<Competition> GetCompetition(int id)
        {
            var competition = _context.Competitions
                .Include(c => c.CompetitionDivers)
                .ThenInclude(cd => cd.Diver)
                .Include(c => c.CompetitionJudges)
                .ThenInclude(cj => cj.Judge)
                .FirstOrDefault(c => c.CompetitionId == id);

            if (competition == null)
            {
                return NotFound();
            }

            return competition;
        }

        // PUT: api/Competition/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCompetition(int id, [FromBody] Competition competition)
        {
            if (id != competition.CompetitionId)
            {
                return BadRequest("El ID de la competición en el cuerpo no coincide con el ID en la URL");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(competition).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Competitions.Any(c => c.CompetitionId == id))
                {
                    return NotFound("Competición no encontrada");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Competition/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCompetition(int id)
        {
            var competition = _context.Competitions.Find(id);

            if (competition == null)
            {
                return NotFound("Competición no encontrada");
            }

            _context.Competitions.Remove(competition);
            _context.SaveChanges();

            return NoContent();
        }

        // Método para generar un código único
        private string GenerateUniqueCode()
        {
            const int codeLength = 6; // Longitud del código único
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string code;
            bool codeExists;

            do
            {
                // Generar un código aleatorio
                code = new string(Enumerable.Repeat(chars, codeLength)
                  .Select(s => s[new Random().Next(s.Length)]).ToArray());

                // Verificar si el código ya existe en la base de datos
                codeExists = _context.Competitions.Any(c => c.Code == code);
            } while (codeExists);

            return code;
            //return Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); // Ejemplo básico
        }
    }
}
