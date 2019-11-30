using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Angular_project_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Angular_project_backend.Services;

namespace Angular_project_backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private IGebruikerService _gebruikerService;
        private readonly ApiContext _context;

        public PollController(ApiContext context, IGebruikerService gebruikerService)
        {
            _context = context;
            _gebruikerService = gebruikerService;
        }

        // GET: api/Polls
        [Authorize]
        [HttpGet]
        public IEnumerable<Poll> GetPolls()
        {
            return _context.Polls;
        }

        // GET: api/Polls/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var poll = await _context.Polls
            //    .Include(p => p.Antwoorden)
            //    .SingleAsync(p => p.PollID == id);

            var poll = await _context.Polls.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        // PUT: api/Polls/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoll([FromRoute] int id, [FromBody] Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poll.PollID)
            {
                return BadRequest();
            }

            _context.Entry(poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        // POST: api/Polls
        [HttpPost]
        public async Task<IActionResult> PostPoll([FromBody] Poll poll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Polls.Add(poll);

            foreach (Antwoord antwoord in poll.Antwoorden)
            {
                _context.Antwoorden.Add(antwoord);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoll", new { id = poll.PollID }, poll);
        }

        // DELETE: api/Polls/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoll([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = await _context.Polls.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();

            return Ok(poll);
        }

        [Authorize]
        [HttpGet("GetPollWhereGebruikerID/{id}")]
        public async Task<IActionResult> GetPollWhereGebruikerID([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pollsByGebruikerID = await _context.Polls
                //.Include(m => m.Gebruiker)
                .Where(m => m.GebruikerID == id)
                .OrderBy(m => m.AanmaakDatum).ToListAsync();


            if (pollsByGebruikerID == null)
            {
                return NotFound();
            }

            return Ok(pollsByGebruikerID);
        }

        // GET: api/Polls/5
        [Authorize]
        [HttpGet("getPollWithAntwoorden/{id}")]
        public async Task<IActionResult> getPollWithAntwoorden([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var poll = await _context.Polls
                .Include(p => p.Antwoorden).Select(p =>
                    new Poll()
                    {
                        PollID = p.PollID,
                        Titel = p.Titel,
                        Beschrijving = p.Beschrijving,
                        AanmaakDatum = p.AanmaakDatum,
                        GebruikerID = p.GebruikerID,
                        Antwoorden = p.Antwoorden,
                        Gebruiker = p.Gebruiker
                    }).SingleAsync(p => p.PollID == id);

            if (poll == null)
            {
                return NotFound();
            }

            return Ok(poll);
        }

        private bool PollExists(int id)
        {
            return _context.Polls.Any(e => e.PollID == id);
        }
    }
}