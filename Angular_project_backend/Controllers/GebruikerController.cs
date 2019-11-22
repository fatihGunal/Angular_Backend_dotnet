using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Angular_project_backend.Models;
using Angular_project_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Angular_project_backend.Services;

namespace Angular_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private IGebruikerService _gebruikerService;
        private readonly ApiContext _context;
        
        public GebruikerController(ApiContext context, IGebruikerService gebruikerService)
        {
            _context = context;
            _gebruikerService = gebruikerService;
        }

        // GET: api/Gebruiker
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gebruiker>>> GetGebruikers()
        {
            return await _context.Gebruikers.ToListAsync();
        }

        // GET: api/Gebruiker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGebruiker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = await _context.Gebruikers.FindAsync(id);

            if (gebruiker == null)
            {
                return NotFound();
            }

            return Ok(gebruiker);
        }

        // PUT: api/Gebruiker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGebruiker([FromRoute] int id, [FromBody] Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gebruiker.GebruikerID)
            {
                return BadRequest();
            }

            _context.Entry(gebruiker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GebruikerExists(id))
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

        // POST: api/Gebruiker
        [HttpPost]
        public async Task<IActionResult> PostGebruiker([FromBody] Gebruiker gebruiker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Gebruikers.Add(gebruiker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGebruiker", new { id = gebruiker.GebruikerID }, gebruiker);
        }

        // DELETE: api/Gebruiker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGebruiker([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gebruiker = await _context.Gebruikers.FindAsync(id);
            if (gebruiker == null)
            {
                return NotFound();
            }

            _context.Gebruikers.Remove(gebruiker);
            await _context.SaveChangesAsync();

            return Ok(gebruiker);
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Gebruiker gebruikerParam)
        {
            var gebruiker = _gebruikerService.Authenticate(gebruikerParam.Gebruikersnaam, gebruikerParam.Wachtwoord);

            if (gebruiker == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(gebruiker);
        }

        private bool GebruikerExists(int id)
        {
            return _context.Gebruikers.Any(e => e.GebruikerID == id);
        }
    }
}