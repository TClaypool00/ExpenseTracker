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
    public class BudgetsController : ControllerBase
    {
        private readonly IBudgetRepository _repo;
        private readonly IUserRepository _userRepo;

        public BudgetsController(IBudgetRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        // GET: api/Budgets
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiBudget>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBudget([FromQuery] string search = null)
        {
            var budgets = new List<ApiBudget>();

            if (budgets == null)
                return NotFound("No budgets found");
            if (search != null)
                budgets = (await _repo.GetBudgetsAsync(search)).Select(ApiMapper.MapBudgets).ToList();
            else
                budgets = (await _repo.GetBudgetsAsync()).Select(ApiMapper.MapBudgets).ToList();

            try
            {
                return Ok(budgets);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiBudget), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetBudget(int id)
        {
            if(await _repo.GetBudgetById(id) is CoreBudget budget)
            {
                var transformed = ApiMapper.MapBudgets(budget);

                return Ok(transformed);
            }

            return NotFound("No budgets found");
        }

        // PUT: api/Budgets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBudget(int id, ApiBudget budget)
        {
            if (id != budget.BudgetId)
            {
                return BadRequest("Budget does not exist.");
            }

            var resource = new CoreBudget
            {
                BudgetId = budget.BudgetId,
                TotalAmtBills = budget.TotalAmtBills,
                User = (await _userRepo.GetUserById(budget.UserId))
            };

            try
            {
                await _repo.UpdateBudgetAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _repo.BudgetExistAsync(id))
                    return NotFound("Budget not found.");
                else
                    throw;
            }

            return Ok("Budget updated!");
        }

        // POST: api/Budgets
        [HttpPost]
        public async Task<ActionResult> PostBudget(ApiBudget budget)
        {
            try
            {
                var resource = new CoreBudget
                {
                    BudgetId = budget.BudgetId,
                    TotalAmtBills = budget.TotalAmtBills,
                    User = (await _userRepo.GetUserById(budget.UserId))
                };

                await _repo.AddBudgetAsync(resource);

                return Ok("Budget has been added!");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBudget(int id)
        {
            try
            {
                if(await _repo.GetBudgetById(id) is CoreBudget budget)
                {
                    await _repo.RemoveBudgetAsync(budget.BudgetId);
                    return Ok("Budget has been removed.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return NotFound("Budget does not exist");
        }
    }
}
