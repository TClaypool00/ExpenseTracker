using ExpenseTracker.App.ApiModels;
using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _repo;
        private readonly IUserRepository _userRepo;

        public BillsController(IBillRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        // GET: api/Bills
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiBills>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBills([FromQuery] string search = null)
        {
            var bills = new List<ApiBills>();

            if (bills == null)
                return NotFound("There are not bills");

            if (search != null)
                bills = (await _repo.GetBillsAsync(search)).Select(ApiMapper.MapBills).ToList();
            else
                bills = (await _repo.GetBillsAsync()).Select(ApiMapper.MapBills).ToList();

            try
            {
                return Ok(bills);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiBills), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBills(int id)
        {
            if(await _repo.GetBillById(id) is CoreBills bill)
            {
                var transformed = ApiMapper.MapBills(bill);

                return Ok(transformed);
            }

            return NotFound("No Bills found");
        }

        // PUT: api/Bills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBills(int id, ApiBills bills)
        {
            if (id != bills.BillId)
            {
                return BadRequest("Bill does not Exist");
            }

            var resource = new CoreBills
            {
                BillId = bills.BillId,
                BillLate = bills.BillLate,
                BillName = bills.BillName,
                BillPrice = bills.BillPrice,
                DueDate = bills.DueDate,
                User = await _userRepo.GetUserById(bills.UserId)
            };

            try
            {
                await _repo.UpdateBillAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repo.BillExistAsync(id))
                    return NotFound("Bill not found");
                else
                    throw;
            }

            return Ok("Bill updated!");
        }

        // POST: api/Bills
        [HttpPost]
        public async Task<ActionResult> PostBills(ApiBills bills)
        {
            try
            {
                var resource = new CoreBills
                {
                    BillId = bills.BillId,
                    BillLate = bills.BillLate,
                    BillName = bills.BillName,
                    BillPrice = bills.BillPrice,
                    DueDate = bills.DueDate,
                    User = await _userRepo.GetUserById(bills.UserId)
                };

                await _repo.AddBillAsync(resource);

                return Ok("Bill had been added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBills(int id)
        {
            try
            {
                if (await _repo.GetBillById(id) is CoreBills bill)
                {
                    await _repo.RemoveBillAsync(bill.BillId);
                    return Ok("Bill has been removed");
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            return NotFound("Bill does not exist");
        }
    }
}
