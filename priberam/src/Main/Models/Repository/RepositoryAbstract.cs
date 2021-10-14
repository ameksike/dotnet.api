using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using priberam.Models.DTO;

namespace priberam.Models.Repository
{
    public class RepositoryAbstract<TEntity, TContext> : RepositoryInterface<TEntity>
        where TEntity : class, EntityInterface
        where TContext : DbContext
    {
        private readonly TContext _context;

        public RepositoryAbstract(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<TEntity>> List()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Select(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(entity.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return entity;
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public bool Exists(int id)
        {
            return _context.Set<TEntity>().Any(e => e.Id == id);
        }
    }
}
