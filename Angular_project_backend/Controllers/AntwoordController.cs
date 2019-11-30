using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Angular_project_backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace Angular_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntwoordController : ControllerBase
    {
        private readonly ApiContext _context;

        public AntwoordController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Antwoord
        [HttpGet]
        public IEnumerable<Antwoord> GetAntwoorden()
        {
            return _context.Antwoorden;
        }

        // GET: api/Antwoord/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAntwoord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var antwoord = await _context.Antwoorden.FindAsync(id);

            if (antwoord == null)
            {
                return NotFound();
            }

            return Ok(antwoord);
        }

        // PUT: api/Antwoord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntwoord([FromRoute] int id, [FromBody] Antwoord antwoord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != antwoord.AntwoordID)
            {
                return BadRequest();
            }

            _context.Entry(antwoord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntwoordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Antwoord
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAntwoord([FromBody] Antwoord antwoord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Antwoorden.Add(antwoord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAntwoord", new { id = antwoord.AntwoordID }, antwoord);
        }

        // DELETE: api/Antwoord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAntwoord([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var antwoord = await _context.Antwoorden.FindAsync(id);
            if (antwoord == null)
            {
                return NotFound();
            }

            _context.Antwoorden.Remove(antwoord);
            await _context.SaveChangesAsync();

            return Ok(antwoord);
        }

        private bool AntwoordExists(int id)
        {
            return _context.Antwoorden.Any(e => e.AntwoordID == id);
        }
    }
}