using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rotageek.Models;

namespace rotageek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly RotageekContext _context;

        public ContactMessagesController(RotageekContext context)
        {
            _context = context;
        }

        // GET: api/ContactMessages
        [HttpGet]
        public IEnumerable<ContactMessage> GetContactMessages()
        {
            return _context.ContactMessages;
        }

        // GET: api/ContactMessages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactMessage = await _context.ContactMessages.FindAsync(id);

            if (contactMessage == null)
            {
                return NotFound();
            }

            return Ok(contactMessage);
        }

        // PUT: api/ContactMessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactMessage([FromRoute] Guid id, [FromBody] ContactMessage contactMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactMessage.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactMessageExists(id))
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

        // POST: api/ContactMessages
        [HttpPost]
        public async Task<IActionResult> PostContactMessage([FromBody] ContactMessage contactMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ContactMessages.Add(contactMessage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactMessage", new { id = contactMessage.Id }, contactMessage);
        }

        // DELETE: api/ContactMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactMessage = await _context.ContactMessages.FindAsync(id);
            if (contactMessage == null)
            {
                return NotFound();
            }

            _context.ContactMessages.Remove(contactMessage);
            await _context.SaveChangesAsync();

            return Ok(contactMessage);
        }

        private bool ContactMessageExists(Guid id)
        {
            return _context.ContactMessages.Any(e => e.Id == id);
        }
    }
}