using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using priberam.Models.DTO;
using priberam.Models.Repository;

namespace priberam.Controllers
{
    [Route("api/[controller]")] 
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class CandidateController : AbstractApiController<Candidate, RepositoryInterface<Candidate>>
    {
        public CandidateController(RepositoryInterface<Candidate> repository) : base(repository)
        {

        }
    }
}