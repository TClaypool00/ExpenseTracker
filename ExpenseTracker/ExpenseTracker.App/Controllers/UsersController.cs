using ExpenseTracker.App.ApiModels;
using ExpenseTracker.Core.CoreModels;
using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly AppSettings _settings;

        public UsersController(IUserRepository repo, IOptions<AppSettings> settings)
        {
            _repo = repo;
            _settings = settings.Value;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(List<ApiUsers>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUsers([FromQuery] string search = null)
        {
            var users = new List<ApiUsers>();

            if (search == null)
                users = (await _repo.GetUsersAsync()).Select(ApiMapper.MapUsers).ToList();
            else
                users = (await _repo.GetUsersAsync(search)).Select(ApiMapper.MapUsers).ToList();

            try
            {
                if (users.Count == 0 && search == null)
                    return Ok("There are no Users");
                else if (users.Count == 0 && search != null)
                    return NotFound($"No users with search parameters '{search}' found.");
                else
                    return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiUsers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUsers(int id)
        {
            try
            {
                if (await _repo.GetUserById(id) is CoreUsers sub)
                {
                    var resource = ApiMapper.MapUsers(sub);
                    return Ok(resource);
                }
            }
            catch (NullReferenceException)
            {
                return NotFound($"No Users with an id of {id} was found.");
            }

            return Ok("There are no Users.");
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUsers(int id, ApiUsers users)
        {
            if (id != users.UserId)
            {
                return BadRequest("User does not exist.");
            }

            var resource = ApiMapper.MapUsers(users);

            try
            {
                await _repo.UpdateUserAsync(resource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repo.UserExistAsync(id))
                {
                    return NotFound("User not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok("User updated.");
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PostUsers(ApiUsers users)
        {
            try
            {
                var resource = ApiMapper.MapUsers(users);

                await _repo.AddUserAsync(resource);
                return Ok("User added!");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUsers(int id)
        {
            try
            {
                if (await _repo.GetUserById(id) is CoreUsers user)
                {
                    await _repo.RemoveUserAsync(user.UserId);
                    return Ok("User has been deleted.");
                }
            }
            catch (NullReferenceException)
            {
                return BadRequest($"User with id of {id} does not exist.");
            }

            return NotFound("User does exist.");
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(ApiUsers), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticateModel model)
        {
            CoreUsers user;
            try
            {
                user = await _repo.GetUserByEmail(model.Email);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (user == null)
                return BadRequest("User does not exist");

            if (user.Password != model.Password)
                return BadRequest("Password does not match system");

            var tokenHander = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var tokenDescritpor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHander.CreateToken(tokenDescritpor);
            var tokenString = tokenHander.WriteToken(token);

            return Ok(new ApiUsers
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                Street = user.Street,
                City = user.City,
                State = user.State,
                Zip = user.Zip,
                PhoneNumber = user.PhoneNumber,
                Token = tokenString
            });
        }
    }
}
