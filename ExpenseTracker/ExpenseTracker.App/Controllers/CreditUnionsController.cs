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
    public class CreditUnionsController : ControllerBase
    {
        private readonly ShoelessJoeContext _context;

        public CreditUnionsController(ShoelessJoeContext context)
        {
            _context = context;
        }

        // GET: api/CreditUnions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditUnion>>> GetCreditUnion()
        {
            return await _context.CreditUnion.ToListAsync();
        }

        // GET: api/CreditUnions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditUnion>> GetCreditUnion(int id)
        {
            var creditUnion = await _context.CreditUnion.FindAsync(id);

            if (creditUnion == null)
            {
                return NotFound();
            }

            return creditUnion;
        }

        // PUT: api/CreditUnions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditUnion(int id, CreditUnion creditUnion)
        {
            if (id != creditUnion.UnionId)
            {
                return BadRequest();
            }

            _context.Entry(creditUnion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditUnionExists(id))
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

        // POST: api/CreditUnions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CreditUnion>> PostCreditUnion(CreditUnion creditUnion)
        {
            _context.CreditUnion.Add(creditUnion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditUnion", new { id = creditUnion.UnionId }, creditUnion);
        }

        // DELETE: api/CreditUnions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreditUnion>> DeleteCreditUnion(int id)
        {
            var creditUnion = await _context.CreditUnion.FindAsync(id);
            if (creditUnion == null)
            {
                return NotFound();
            }

            _context.CreditUnion.Remove(creditUnion);
            await _context.SaveChangesAsync();

            return creditUnion;
        }

        private bool CreditUnionExists(int id)
        {
            return _context.CreditUnion.Any(e => e.UnionId == id);
        }
    }
}
