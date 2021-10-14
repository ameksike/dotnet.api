using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using priberam.Models.DTO;
using priberam.Services;

namespace priberam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IdentityServiceInterface _identityService;
        private readonly AccountServiceInterface _accountService;

        public AccountController(
            IdentityServiceInterface identityService,
            AccountServiceInterface accountService
        )
        {
            _accountService = accountService;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AccountUser model)
        {
            if (ModelState.IsValid)
            {
                if ( await _accountService.Create(model) )
                {
                    return Ok(_identityService.BuildToken(model)); 
                }
                else
                {
                    return BadRequest("Invalid Account User Data");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _accountService.List();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string Email)
        {
            var entity = await _accountService.Select( new AccountUser { Email = Email} );
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        /*[HttpPut("{id}")]
        public async Task<IActionResult> Put(string Email, AccountUser entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            var result = await _accountService.Update(entity);
            return Ok(result);
            return null;
        }*/

        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string Email)
        {
            var entity = await _accountService.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
            return null;
        }*/

    }
}