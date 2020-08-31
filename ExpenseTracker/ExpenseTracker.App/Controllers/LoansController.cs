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
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly ICreditUnionRepository _unionRepo;

        public LoansController(ILoanRepository repo, IUserRepository userRepo, ICreditUnionRepository unionRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _unionRepo = unionRepo;
        }

        // GET: api/Loans
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiLoan>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetLoan([FromQuery] int userId = 0, string search = null)
        {
            var loans = new List<ApiLoan>();

            if (userId == 0 && search == null)
                loans = (await _repo.GetLoanAsync()).Select(ApiMapper.MapLoan).ToList();
            else
                loans = (await _repo.GetLoanAsync(search, userId)).Select(ApiMapper.MapLoan).ToList();

            try
            {
                if (loans.Count == 0 && search == null && userId == 0)
                    return Ok("There are no Loans.");
                else if (loans.Count == 0 && search != null && userId != 0)
                    return NotFound($"There are no Loans with userId of {userId} and search parameter of '{search}'.");
                else if (loans.Count == 0 && userId != 0)
                    return NotFound($"There are no Loans with the user Id of {userId}.");
                else if (loans.Count == 0 && search != null)
                    return NotFound($"There are Loans with '{search}'.");
                else
                    return Ok(loans);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong.");
            }
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiLoan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetLoan(int id)
        {
            try
            {
                if(await _repo.GetLoanById(id) is CoreLoan loan)
                {
                    var transformed = ApiMapper.MapLoan(loan);

                    return Ok(transformed);
                }
            }
            catch (NullReferenceException)
            {
                return NotFound($"No loan with the id of {id}.");
            }
            return Ok("There is no loan.");
        }

        // PUT: api/Loans/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiLoan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutLoan(int id, ApiLoan loan)
        {
            if (id != loan.LoanId)
            {
                return BadRequest("Loan does not exist.");
            }

            var resource = new CoreLoan
            {
                LoanId = loan.LoanId,
                Deposit = loan.Deposit,
                MonthlyAmountDue = loan.MonthlyAmountDue,
                PaymentDueDate = loan.PaymentDueDate,
                TotalAmountDue = loan.TotalAmountDue,
                User = (await _userRepo.GetUserById(loan.UserId)),
                Union = (await _unionRepo.GetCreditUnionById(loan.UnionId))
            };

            try
            {
                await _repo.UpdateLoanAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.LoanExistAsync(id))
                    return NotFound("Loan not found.");
                else
                    throw;
            }

            return Ok("Loan updated!");
        }

        // POST: api/Loans
        [HttpPost]
        [ProducesResponseType(typeof(ApiLoan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostLoan(ApiLoan loan)
        {
            try
            {
                var resource = new CoreLoan
                {
                    LoanId = loan.LoanId,
                    Deposit = loan.Deposit,
                    MonthlyAmountDue = loan.MonthlyAmountDue,
                    PaymentDueDate = loan.PaymentDueDate,
                    TotalAmountDue = loan.TotalAmountDue,
                    User = (await _userRepo.GetUserById(loan.UserId)),
                    Union = (await _unionRepo.GetCreditUnionById(loan.UnionId))
                };

                await _repo.AddLoanAsync(resource);

                return Ok("Loan added!");
            }
            catch(Exception e)
            {
                return BadRequest($"Error! {e.Message}");
            }
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiLoan), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteLoan(int id)
        {
            try
            {
                if(await _repo.GetLoanById(id) is CoreLoan loan)
                {
                    await _repo.RemoveLoanAsync(loan.LoanId);
                    return Ok("Bill has been deleted.");
                }
            }
            catch (NullReferenceException)
            {
                return BadRequest($"Loan with id of {id} does not exist.");
            }
            return NotFound("Bill does not exist.");
        }
    }
}
