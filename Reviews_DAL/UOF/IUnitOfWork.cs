using Catalog_DAL.Repositories.Contracts;

namespace Catalog_DAL.UOF
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }

}
