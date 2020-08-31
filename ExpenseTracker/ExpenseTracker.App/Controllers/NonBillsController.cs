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
    public class NonBillsController : ControllerBase
    {
        private readonly INonBillsRepository _repo;
        private readonly IUserRepository _userRepo;

        public NonBillsController(INonBillsRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        // GET: api/NonBills
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiBills>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetNonBills([FromQuery] int userId = 0, string search = null)
        {
            var bills = new List<ApiNonBills>();

            if (userId == 0 && search == null)
                bills = (await _repo.GetNonBillsAsync()).Select(ApiMapper.MapNonBills).ToList();
            else
                bills = (await _repo.GetNonBillsAsync(userId, search)).Select(ApiMapper.MapNonBills).ToList();
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/NonBills/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiNonBills), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetNonBills(int id)
        {
            try
            {
                if(await _repo.GetNonBillById(id) is CoreNonBills bill)
                {
                    var resource = ApiMapper.MapNonBills(bill);
                    return Ok(resource);
                }
            }
            catch(NullReferenceException)
            {
                return NotFound($"No Non-bill with the id of {id}.");
            }

            return Ok("There are no Non-bills.");
        }

        // PUT: api/NonBills/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiNonBills), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutNonBills(int id, ApiNonBills nonBills)
        {
            if (id != nonBills.NonBillId)
            {
                return BadRequest("Non-bill does not exist.");
            }

            var resource = new CoreNonBills
            {
                NonBillId = nonBills.NonBillId,
                Price = nonBills.Price,
                StoreName = nonBills.StoreName,
                User = await _userRepo.GetUserById(nonBills.UserId)
            };

            try
            {
                await _repo.UpdateNonBillAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.NonBillExistAsync(id))
                {
                    return NotFound("Non-bill does not exist.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Non-bill updated!");
        }

        // POST: api/NonBills
        [HttpPost]
        [ProducesResponseType(typeof(ApiNonBills), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostNonBills(ApiNonBills nonBills)
        {
            try
            {
                var resource = new CoreNonBills
                {
                    NonBillId = nonBills.NonBillId,
                    Price = nonBills.Price,
                    StoreName = nonBills.StoreName,
                    User = await _userRepo.GetUserById(nonBills.UserId)
                };

                await _repo.AddNonBillAsync(resource);

                return Ok("Non-bill has been added!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/NonBills/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiNonBills), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteNonBills(int id)
        {
            try
            {
                if(await _repo.GetNonBillById(id) is CoreNonBills bill)
                {
                    await _repo.RemoveNonBillAsync(bill.NonBillId);
                    return Ok("Non-bill has been removed.");
                }
            }
            catch (NullReferenceException)
            {
                return BadRequest($"Non-bill with id of {id} does not exist.");
            }

            return NotFound("Non-bill does not exist.");
        }
    }
}
