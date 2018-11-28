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
    [Route("api/Strategies")]
    public class StrategiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StrategiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Strategies
        [HttpGet]
        public IEnumerable<Strategy> GetStrategies()
        {
            return _context.Strategies;
        }

        // GET: api/Strategies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStrategy([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strategy = await _context.Strategies.SingleOrDefaultAsync(m => m.Id == id);

            if (strategy == null)
            {
                return NotFound();
            }

            return Ok(strategy);
        }

        // PUT: api/Strategies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStrategy([FromRoute] Guid id, [FromBody] Strategy strategy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != strategy.Id)
            {
                return BadRequest();
            }

            _context.Entry(strategy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrategyExists(id))
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

        // POST: api/Strategies
        [HttpPost]
        public async Task<IActionResult> PostStrategy([FromBody] Strategy strategy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Strategies.Add(strategy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStrategy", new { id = strategy.Id }, strategy);
        }

        // DELETE: api/Strategies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStrategy([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var strategy = await _context.Strategies.SingleOrDefaultAsync(m => m.Id == id);
            if (strategy == null)
            {
                return NotFound();
            }

            _context.Strategies.Remove(strategy);
            await _context.SaveChangesAsync();

            return Ok(strategy);
        }

        private bool StrategyExists(Guid id)
        {
            return _context.Strategies.Any(e => e.Id == id);
        }
    }
}