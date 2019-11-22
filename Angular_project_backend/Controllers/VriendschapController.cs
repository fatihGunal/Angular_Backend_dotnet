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
    public class VriendschapController : ControllerBase
    {
        private readonly ApiContext _context;

        public VriendschapController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Vriendschap
        [HttpGet]
        public IEnumerable<Vriendschap> GetVriendschappen()
        {
            return _context.Vriendschappen;
        }

        // GET: api/Vriendschap/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVriendschap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vriendschap = await _context.Vriendschappen.FindAsync(id);

            if (vriendschap == null)
            {
                return NotFound();
            }

            return Ok(vriendschap);
        }

        // PUT: api/Vriendschap/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVriendschap([FromRoute] int id, [FromBody] Vriendschap vriendschap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vriendschap.VriendschapID)
            {
                return BadRequest();
            }

            _context.Entry(vriendschap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VriendschapExists(id))
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

        // POST: api/Vriendschap
        [HttpPost]
        public async Task<IActionResult> PostVriendschap([FromBody] Vriendschap vriendschap)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vriendschappen.Add(vriendschap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVriendschap", new { id = vriendschap.VriendschapID }, vriendschap);
        }

        // DELETE: api/Vriendschap/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVriendschap([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vriendschap = await _context.Vriendschappen.FindAsync(id);
            if (vriendschap == null)
            {
                return NotFound();
            }

            _context.Vriendschappen.Remove(vriendschap);
            await _context.SaveChangesAsync();

            return Ok(vriendschap);
        }

        private bool VriendschapExists(int id)
        {
            return _context.Vriendschappen.Any(e => e.VriendschapID == id);
        }
    }
}