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
    [Route("api/PNLs")]
    public class PNLsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PNLsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PNLs
        [HttpGet]
        public IEnumerable<PNL> GetPNLs()
        {
            return _context.PNLs;
        }

        // GET: api/PNLs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPNL([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pNL = await _context.PNLs.SingleOrDefaultAsync(m => m.Id == id);

            if (pNL == null)
            {
                return NotFound();
            }

            return Ok(pNL);
        }

        // PUT: api/PNLs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPNL([FromRoute] Guid id, [FromBody] PNL pNL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pNL.Id)
            {
                return BadRequest();
            }

            _context.Entry(pNL).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PNLExists(id))
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

        // POST: api/PNLs
        [HttpPost]
        public async Task<IActionResult> PostPNL([FromBody] PNL pNL)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PNLs.Add(pNL);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPNL", new { id = pNL.Id }, pNL);
        }

        // DELETE: api/PNLs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePNL([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pNL = await _context.PNLs.SingleOrDefaultAsync(m => m.Id == id);
            if (pNL == null)
            {
                return NotFound();
            }

            _context.PNLs.Remove(pNL);
            await _context.SaveChangesAsync();

            return Ok(pNL);
        }

        private bool PNLExists(Guid id)
        {
            return _context.PNLs.Any(e => e.Id == id);
        }
    }
}