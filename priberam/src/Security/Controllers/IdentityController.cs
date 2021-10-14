using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using priberam.Models.DTO;
using priberam.Services;

namespace priberam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IdentityServiceInterface _identityService;

        public IdentityController( IdentityServiceInterface identityService )
        {
            _identityService = identityService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AccountUser User)
        {
            if (ModelState.IsValid)
            {
                if ( await _identityService.isValidAccount(User) )
                {
                    return Ok( _identityService.BuildToken(User) ); 
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}