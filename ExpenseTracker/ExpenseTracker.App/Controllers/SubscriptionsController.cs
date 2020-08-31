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
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionsRepository _repo;
        private readonly IUserRepository _userRepo;

        public SubscriptionsController(ISubscriptionsRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        // GET: api/Subscriptions
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiSubscriptions>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSubscriptions([FromQuery] string userId = null, string search = null)
        {
            var subs = new List<ApiSubscriptions>();

            if (userId == null && search == null)
                subs = (await _repo.GetSubscriptionsAsync()).Select(ApiMapper.MapSub).ToList();
            else
                subs = (await _repo.GetSubscriptionsAsync(userId, search)).Select(ApiMapper.MapSub).ToList();

            try
            {
                if (subs.Count == 0 && search == null && userId == null)
                    return Ok("There are no bills.");
                else if (subs.Count == 0 && search != null && userId != null)
                    return NotFound($"There are no bills with userId of {userId} and search parameter of '{search}'.");
                else if (subs.Count == 0 && userId != null)
                    return NotFound($"There are no bills with the user Id of {userId}.");
                else if (subs.Count == 0 && search != null)
                    return NotFound($"There are bills with '{search}'.");
                else
                    return Ok(subs);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Subscriptions/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiSubscriptions), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetSubscriptions(int id)
        {
            try
            {
                if(await _repo.GetSubscriptionById(id) is CoreSubscriptions sub)
                {
                    var resource = ApiMapper.MapSub(sub);
                    return Ok(resource);
                }
            }
            catch (NullReferenceException)
            {
                return NotFound($"No Subscripttion with an id of {id} was found.");
            }

            return Ok("There are no Subscriptions.");
        }

        // PUT: api/Subscriptions/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutSubscriptions(int id, ApiSubscriptions sub)
        {
            if (id != sub.SubId)
            {
                return BadRequest("Subscription does not exist.");
            }

            var resource = new CoreSubscriptions
            {
                SubId = sub.SubId,
                AmountDue = sub.AmountDue,
                DueDate = sub.DueDate,
                CompanyName = sub.CompanyName,
                User = await _userRepo.GetUserById(sub.UserId)
            };

            try
            {
                await _repo.UpdateSubscriptionAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.SubscriptionExistAsync(id))
                {
                    return NotFound("Subscrption not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Subscrption updated!");
        }

        // POST: api/Subscriptions
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostSubscriptions(ApiSubscriptions sub)
        {
            try
            {
                var resource = new CoreSubscriptions
                {
                    SubId = sub.SubId,
                    AmountDue = sub.AmountDue,
                    DueDate = sub.DueDate,
                    CompanyName = sub.CompanyName,
                    User = await _userRepo.GetUserById(sub.UserId)
                };

                await _repo.AddSubscriptionAsync(resource);
                return Ok("Subscription added!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Subscriptions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteSubscriptions(int id)
        {
            try
            {
                if(await _repo.GetSubscriptionById(id) is CoreSubscriptions sub)
                {
                    await _repo.RemoveSubscriptionAsync(sub.SubId);
                    return Ok("Subscrption has been deleted.");
                }
            }
            catch (NullReferenceException)
            {
                return BadRequest($"SubScrption with id of {id} does not exist.");
            }

            return NotFound("Subscrption does exist.");
        }
    }
}
