using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using priberam.Models.DTO;
using priberam.Models.Repository;

namespace priberam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public abstract class AbstractApiController<TEntity, TRepository> : ControllerBase
        where TEntity : class, EntityInterface
        where TRepository : RepositoryInterface<TEntity>
    {
        private readonly TRepository _repository;

        public AbstractApiController(TRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _repository.List();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await _repository.Select(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }
            var result = await _repository.Update(entity);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TEntity entity)
        {
            var result = await _repository.Create(entity);
            return Ok(result); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

    }
}