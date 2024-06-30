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

        // Método para generar un código único (puedes ajustarlo según tus necesidades)
        private string GenerateUniqueCode()
        {
            const int codeLength = 6; // Longitud del código único
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Caracteres permitidos para el código

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
