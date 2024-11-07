using Catalog_DAL.Entities;
using Catalog_DAL.Repositories.Contracts;

namespace Catalog_DAL.UOF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogContext _context;
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWork(
            CatalogContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
