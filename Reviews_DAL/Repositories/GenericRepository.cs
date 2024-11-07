﻿using Catalog_DAL.Entities;
using Catalog_DAL.Repositories.Contracts;
using Catalog_DAL.Specification;
using Microsoft.EntityFrameworkCore;

namespace Catalog_DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CatalogContext context;
        protected readonly DbSet<T> table;

        public GenericRepository(CatalogContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            var addedEntity = await table.AddAsync(entity);
            return addedEntity.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            var updatedEntity = context.Update(entity);
            return await Task.Run(() => updatedEntity.Entity);
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await Task.Run(() => table.Remove(entity));
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException();
            }
            await Task.Run(() => table.Remove(entity));
        }

        public async Task<IEnumerable<T>> FindWithSpecification(ISpecification<T> specification)
        {
            return await Task.Run(() => SpecificationEvaluator<T>.GetQuery(table.AsQueryable(), specification));
        }
    }
}
