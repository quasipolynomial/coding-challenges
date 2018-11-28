using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GSA.Data;
using GSA.Model;

namespace GSA.Controllers
{
    [Produces("application/json")]
    [Route("api/Capitals")]
    public class CapitalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CapitalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Capitals
        [HttpGet]
        public IEnumerable<Capital> GetCapitals()
        {
            return _context.Capitals;
        }

        // GET: api/Capitals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCapital([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var capital = await _context.Capitals.SingleOrDefaultAsync(m => m.Id == id);

            if (capital == null)
            {
                return NotFound();
            }

            return Ok(capital);
        }

        // PUT: api/Capitals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCapital([FromRoute] Guid id, [FromBody] Capital capital)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != capital.Id)
            {
                return BadRequest();
            }

            _context.Entry(capital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CapitalExists(id))
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

        // POST: api/Capitals
        [HttpPost]
        public async Task<IActionResult> PostCapital([FromBody] Capital capital)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Capitals.Add(capital);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCapital", new { id = capital.Id }, capital);
        }

        // DELETE: api/Capitals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCapital([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var capital = await _context.Capitals.SingleOrDefaultAsync(m => m.Id == id);
            if (capital == null)
            {
                return NotFound();
            }

            _context.Capitals.Remove(capital);
            await _context.SaveChangesAsync();

            return Ok(capital);
        }

        private bool CapitalExists(Guid id)
        {
            return _context.Capitals.Any(e => e.Id == id);
        }
    }
}