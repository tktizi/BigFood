﻿using Catalog_DAL.Specification;
namespace Catalog_DAL.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(T entity);

        Task<IEnumerable<T>> FindWithSpecification(ISpecification<T> specification);
    }
}
