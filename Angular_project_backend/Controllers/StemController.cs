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
    public class StemController : ControllerBase
    {
        private readonly ApiContext _context;

        public StemController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Stem
        [HttpGet]
        public IEnumerable<Stem> GetStemmen()
        {
            return _context.Stemmen;
        }

        // GET: api/Stem/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stem = await _context.Stemmen.FindAsync(id);

            if (stem == null)
            {
                return NotFound();
            }

            return Ok(stem);
        }

        // PUT: api/Stem/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStem([FromRoute] int id, [FromBody] Stem stem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stem.StemID)
            {
                return BadRequest();
            }

            _context.Entry(stem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StemExists(id))
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

        // POST: api/Stem
        [HttpPost]
        public async Task<IActionResult> PostStem([FromBody] Stem stem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Stemmen.Add(stem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStem", new { id = stem.StemID }, stem);
        }

        // DELETE: api/Stem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stem = await _context.Stemmen.FindAsync(id);
            if (stem == null)
            {
                return NotFound();
            }

            _context.Stemmen.Remove(stem);
            await _context.SaveChangesAsync();

            return Ok(stem);
        }
        
        [Authorize]
        [HttpGet("getAllStemWithAntwoordWithPoll")]
        public async Task<IActionResult> getAllStemWithAntwoordWithPoll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stemmen = await _context.Stemmen
                .Include(s => s.Antwoord).Select(s =>
                    new Stem()
                    {
                        StemID = s.StemID,
                        Antwoord = s.Antwoord,
                        GebruikerID = s.GebruikerID,
                        PollID = s.PollID
                    }).ToListAsync();
            //var stemmen = await _context.Stemmen
            //    .Include(s => s.Antwoord)
            //    .Where(s => s.GebruikerID == id).ToListAsync();

            if (stemmen == null)
            {
                return NotFound();
            }

            return Ok(stemmen);
        }

        [Authorize]
        [HttpGet("getAllStemWithAntwoordWithPollByGebruiker/{id}")]
        public async Task<IActionResult> getAllStemWithAntwoordWithPollByGebruiker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stemmen = await _context.Stemmen
                .Include(s => s.Antwoord).Select(s =>
                    new Stem()
                    {
                        StemID = s.StemID,
                        Antwoord = s.Antwoord,
                        GebruikerID = s.GebruikerID,
                        PollID = s.PollID
                    })
                    .Where(s => s.GebruikerID == id).ToListAsync();
            //var stemmen = await _context.Stemmen
            //    .Include(s => s.Antwoord)
            //    .Where(s => s.GebruikerID == id).ToListAsync();

            if (stemmen == null)
            {
                return NotFound();
            }

            return Ok(stemmen);
        }

        private bool StemExists(int id)
        {
            return _context.Stemmen.Any(e => e.StemID == id);
        }
    }
}