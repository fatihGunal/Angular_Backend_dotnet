using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Angular_project_backend.Models;

namespace Angular_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollGebruikerController : ControllerBase
    {
        private readonly ApiContext _context;

        public PollGebruikerController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/PollGebruiker
        [HttpGet]
        public IEnumerable<PollGebruiker> GetPollGebruikers()
        {
            return _context.PollGebruikers;
        }

        // GET: api/PollGebruiker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPollGebruiker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);

            if (pollGebruiker == null)
            {
                return NotFound();
            }

            return Ok(pollGebruiker);
        }

        // PUT: api/PollGebruiker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollGebruiker([FromRoute] int id, [FromBody] PollGebruiker pollGebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pollGebruiker.PollGebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(pollGebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollGebruikerExists(id))
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

        // POST: api/PollGebruiker
        [HttpPost]
        public async Task<IActionResult> PostPollGebruiker([FromBody] PollGebruiker pollGebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PollGebruikers.Add(pollGebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollGebruiker", new { id = pollGebruiker.PollGebruikerID }, pollGebruiker);
        }

        // DELETE: api/PollGebruiker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePollGebruiker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pollGebruiker = await _context.PollGebruikers.FindAsync(id);
            if (pollGebruiker == null)
            {
                return NotFound();
            }

            _context.PollGebruikers.Remove(pollGebruiker);
            await _context.SaveChangesAsync();

            return Ok(pollGebruiker);
        }

        private bool PollGebruikerExists(int id)
        {
            return _context.PollGebruikers.Any(e => e.PollGebruikerID == id);
        }
    }
}