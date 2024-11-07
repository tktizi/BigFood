using BigFood_Reviews.Entities;
using BigFood_Reviews.Repositories.Interfaces;
using System.Data;

namespace BigFood_Reviews.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IDbConnection _dbConnection;
        public ICustomerRepository Customers { get; }
        public IReviewRepository Reviews { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(ICustomerRepository customerRepository,
            IReviewRepository reviewRepository,
            IDbTransaction dbTransaction)
        {
            Customers = customerRepository;
            Reviews = reviewRepository;
            _dbTransaction = dbTransaction;
        }

      

        public Task CommitAsync()
        {
            return Task.Run(() => _dbTransaction.Commit());
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }

}
