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
        public async Task<ActionResult> GetBills([FromQuery] int userId = 0, string search = null)
        {
            var bills = new List<ApiBills>();

            if (userId == 0 && search == null)
                bills = (await _repo.GetBillsAsync()).Select(ApiMapper.MapBills).ToList();
            else
                bills = (await _repo.GetBillsAsync(search, userId)).Select(ApiMapper.MapBills).ToList();


            try
                {
                if (bills.Count == 0 && search == null && userId == 0)
                    return Ok("There are no bills.");
                else if (bills.Count == 0 && search != null && userId != 0)
                    return NotFound($"There are no bills with userId of {userId} and search parameter of '{search}'.");
                else if (bills.Count == 0 && userId != 0)
                    return NotFound($"There are no bills with the user Id of {userId}.");
                else if (bills.Count == 0 && search != null)
                    return NotFound($"There are bills with '{search}'.");
                else
                    return Ok(bills);
                }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
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
            try
            {
                if (await _repo.GetBillById(id) is CoreBills bill)
                {
                    var transformed = ApiMapper.MapBills(bill);

                    return Ok(transformed);
                }
            }
            catch (NullReferenceException)
            {
                return NotFound($"No bill with the id of {id}.");
            }

            return Ok("There are no bills.");

        }

        // PUT: api/Bills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBills(int id, ApiBills bills)
        {
            if (id != bills.BillId)
            {
                return BadRequest("Bill does not exist.");
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
                if (!await _repo.BillExistAsync(id))
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
            catch (Exception e)
            {
                return BadRequest($"Something went wrong. {e.Message}");
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
                    return Ok("Bill has been deleted.");
                }
            }
            catch (NullReferenceException)
            {
                return BadRequest($"Bill with id of {id} does not exist.");
            }

            return NotFound("Bill does not exist.");
        }
    }
}
