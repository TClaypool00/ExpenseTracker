using ExpenseTracker.DataAccess.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ShoelessJoeContext _context;

        public SubscriptionsController(ShoelessJoeContext context)
        {
            _context = context;
        }

        // GET: api/Subscriptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriptions>>> GetSubscriptions()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriptions>> GetSubscriptions(int id)
        {
            var subscriptions = await _context.Subscriptions.FindAsync(id);

            if (subscriptions == null)
            {
                return NotFound();
            }

            return subscriptions;
        }

        // PUT: api/Subscriptions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubscriptions(int id, Subscriptions subscriptions)
        {
            if (id != subscriptions.SubId)
            {
                return BadRequest();
            }

            _context.Entry(subscriptions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionsExists(id))
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

        // POST: api/Subscriptions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Subscriptions>> PostSubscriptions(Subscriptions subscriptions)
        {
            _context.Subscriptions.Add(subscriptions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubscriptions", new { id = subscriptions.SubId }, subscriptions);
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subscriptions>> DeleteSubscriptions(int id)
        {
            var subscriptions = await _context.Subscriptions.FindAsync(id);
            if (subscriptions == null)
            {
                return NotFound();
            }

            _context.Subscriptions.Remove(subscriptions);
            await _context.SaveChangesAsync();

            return subscriptions;
        }

        private bool SubscriptionsExists(int id)
        {
            return _context.Subscriptions.Any(e => e.SubId == id);
        }
    }
}
