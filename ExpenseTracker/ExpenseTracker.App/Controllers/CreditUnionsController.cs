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
    public class CreditUnionsController : ControllerBase
    {
        private readonly ICreditUnionRepository _repo;

        public CreditUnionsController(ICreditUnionRepository repo)
        {
            _repo = repo;
        }

        // GET: api/CreditUnions
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiCreditUnion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCreditUnion([FromQuery] string search = null)
        {
            var unions = new List<ApiCreditUnion>();

            if (unions == null)
                return NotFound("The credit union are set to an istance");

            if (search != null)
                unions = (await _repo.GetCreditUnionsAsync(search)).Select(ApiMapper.MapUnion).ToList();
            else
                unions = (await _repo.GetCreditUnionsAsync()).Select(ApiMapper.MapUnion).ToList();

            try
            {
                if (unions.Count == 0)
                    return Ok("There are credit Union");
                else
                    return Ok(unions);
            }
            catch (Exception)
            {
                return StatusCode(500, "Something went wrong");
            }

        }

        // GET: api/CreditUnions/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiCreditUnion), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCreditUnion(int id)
        {
            if(await _repo.GetCreditUnionById(id) is CoreCreditUnion union)
            {
                var transformed = ApiMapper.MapUnion(union);

                return Ok(transformed);
            }

            return NotFound("There is no Credit Union by that Id.");
        }

        // PUT: api/CreditUnions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditUnion(int id, ApiCreditUnion creditUnion)
        {
            if (id != creditUnion.UnionId)
            {
                return BadRequest("Credit Union does not exist.");
            }

            var resource = ApiMapper.MapUnion(creditUnion);

            try
            {
                await _repo.UpdateCreditUnionAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await _repo.CreditUnionExistAsync(id))
                {
                    return NotFound("Credit Union not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Credit Union  updated!");
        }

        // POST: api/CreditUnions
        [HttpPost]
        public async Task<ActionResult> PostCreditUnion(ApiCreditUnion creditUnion)
        {
            try
            {
                var resource = ApiMapper.MapUnion(creditUnion);

                await _repo.AddCreditUnionAsync(resource);

                return Ok("Credit Union has been added!");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        // DELETE: api/CreditUnions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCreditUnion(int id)
        {
            try
            {
                if(await _repo.GetCreditUnionById(id) is CoreCreditUnion union)
                {
                    await _repo.RemoveCreditUnionAsync(union.UnionId);
                    return Ok("Credit Union has been deleted.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
            return NotFound("Credit Union does not exist");
        }
    }
}
