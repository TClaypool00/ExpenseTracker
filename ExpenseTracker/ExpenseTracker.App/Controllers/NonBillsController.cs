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
    public class NonBillsController : ControllerBase
    {
        private readonly ShoelessJoeContext _context;

        public NonBillsController(ShoelessJoeContext context)
        {
            _context = context;
        }

        // GET: api/NonBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NonBills>>> GetNonBills()
        {
            return await _context.NonBills.ToListAsync();
        }

        // GET: api/NonBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NonBills>> GetNonBills(int id)
        {
            var nonBills = await _context.NonBills.FindAsync(id);

            if (nonBills == null)
            {
                return NotFound();
            }

            return nonBills;
        }

        // PUT: api/NonBills/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNonBills(int id, NonBills nonBills)
        {
            if (id != nonBills.NonBillId)
            {
                return BadRequest();
            }

            _context.Entry(nonBills).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NonBillsExists(id))
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

        // POST: api/NonBills
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NonBills>> PostNonBills(NonBills nonBills)
        {
            _context.NonBills.Add(nonBills);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNonBills", new { id = nonBills.NonBillId }, nonBills);
        }

        // DELETE: api/NonBills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NonBills>> DeleteNonBills(int id)
        {
            var nonBills = await _context.NonBills.FindAsync(id);
            if (nonBills == null)
            {
                return NotFound();
            }

            _context.NonBills.Remove(nonBills);
            await _context.SaveChangesAsync();

            return nonBills;
        }

        private bool NonBillsExists(int id)
        {
            return _context.NonBills.Any(e => e.NonBillId == id);
        }
    }
}
